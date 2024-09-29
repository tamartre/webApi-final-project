using AutoMapper;
using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Service;
using System.Net.Http.Headers;

namespace Tamar_Sheva_Project.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IMapper _mapper;
        private IOrderService _orderService;
        public OrderController(IOrderService orderService,IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<OrderReturnDto>> AddOrder([FromBody] OrderDto orderDto)
        {

            Order order = _mapper.Map<OrderDto, Order>(orderDto);
            Order newOrder = await _orderService.AddOrder(order);
           OrderReturnDto returnOrder = _mapper.Map<Order, OrderReturnDto>(newOrder);
            returnOrder.OrderId = order.OrderId;
            if (newOrder != null)
                return Ok(returnOrder);
            return NotFound();

        }

    }
}
