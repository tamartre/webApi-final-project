

using Entities;
using Tamar_Sheva_Project;

namespace Service
{
    public interface IOrderService
    {
        Task<Order> AddOrder(Order orders);
    }
}