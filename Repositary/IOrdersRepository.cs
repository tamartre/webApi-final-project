
using Entities;
using Tamar_Sheva_Project;

namespace Repository
{
    public interface IOrdersRepository
    {
        Task<Order> AddOrder(Order orders);
    }
}