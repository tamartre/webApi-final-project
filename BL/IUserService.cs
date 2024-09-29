

using Entities;
using Tamar_Sheva_Project;

namespace Service
{
    public interface IUserService
    {
        Task<User> Register(User user);
        Task<User> Get(int id);
        Task<User> Login(User user);
        Task<User> Update(int id, User user);
    }
}