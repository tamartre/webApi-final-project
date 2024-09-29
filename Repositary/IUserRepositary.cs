


using Entities;
using Tamar_Sheva_Project;

namespace Repositary
{
    public interface IUserRepositary
    {
        Task<User> Register(User user);
        Task<User> Get(int id);
        Task<User> Login(User user);
        Task<User> Update(int id, User user);

    }
}