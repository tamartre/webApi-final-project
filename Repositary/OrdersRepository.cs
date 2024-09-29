
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamar_Sheva_Project;

namespace Repository
{
    public class OrdersRepository:IOrdersRepository
    {
        AdoNetOurStore326035854Context _furniture;
        public OrdersRepository(AdoNetOurStore326035854Context furniture)
        {
            this._furniture = furniture;
        }
         public async Task<Order> AddOrder(Order order)
        {
          
            await _furniture.Orders.AddAsync(order);
            await _furniture.SaveChangesAsync();
            return order ;
        }
    }
}
