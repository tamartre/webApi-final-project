
using Entities;
using Microsoft.Extensions.Logging;
using Repositary;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamar_Sheva_Project;
using Zxcvbn;

namespace Service;

    public class OrderService : IOrderService
    {
        private IOrdersRepository _repositary;
        private IProductRepositary _prepository;
        private readonly ILogger<OrderService> _logger;
        public OrderService(IOrdersRepository orderService, IProductRepositary productRepositary, ILogger<OrderService> logger)
        {
            this._repositary = orderService;
            this._prepository = productRepositary;
            _logger = logger;
        }
        public async Task<Order> AddOrder(Order order)
        {
            var products = _prepository.Get("", 0, 1000, "", []);
            List<Product> productList = (List<Product>)await products;
            decimal totalSum = (decimal)order.OrderItems
                       .Where(oi => productList.Any(p => p.ProductId == oi.ProductId))
                           .Sum(oi => productList.First(p => p.ProductId == oi.ProductId).Price * oi.Quantity);
            Console.Write("  totalSum  " + totalSum);
            Console.Write("  order.OrderSum  " + order.OrderSum);
            if (order.OrderSum != totalSum)
            {
                Console.WriteLine("ERR");
                _logger.LogError("☠️☠️☠️☠️☠️☠️☠️there is a stoller in the shop");

                return null;
            }
            else
            {
                Order reasult = await _repositary.AddOrder(order);
                if (reasult == null)
                    return null;
                return reasult;
            }
        }
    }

