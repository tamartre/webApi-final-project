
using Entities;
using Repositary;
using System.Collections.Specialized;
using Tamar_Sheva_Project;

namespace Service
{

    public class UserService : IUserService
    {
        IUserRepositary _repositary;
        public UserService(IUserRepositary userService)
        {
            this._repositary = userService;
        }
        public async Task<User> Get(int id)
        {
            User reasult = await _repositary.Get(id);
            if (reasult == null)
                return null;
            return reasult;

        }
        public async Task<User> Register(User user)
        {
            User reasult = await _repositary.Register(user);
            if (reasult == null)
                return null;
            return reasult;


        }
        public async Task<User> Login(User user)
        {
            User reasult = await _repositary.Login(user);
            return reasult;


        }
        public async Task<User> Update(int id, User user)
        {
            User u = await _repositary.Update(id, user);
            return u;
        }

    }
}
