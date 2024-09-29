
using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Linq.Expressions;
using Tamar_Sheva_Project;
//using Repository;

namespace Repositary
{
    public class UserRepositary : IUserRepositary
    {


        AdoNetOurStore326035854Context furniture = new AdoNetOurStore326035854Context();
        public UserRepositary(AdoNetOurStore326035854Context furniture)
        {
            this.furniture = furniture;

        }

        public async Task<User> Get(int id)
        {
            try
            {
                var userDb = await furniture.Users.FirstOrDefaultAsync(e => e.UserId.Equals(id));
                if (userDb == null)
                    return null;
         
                return userDb;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public async Task<User> Register(User user)
        {

            try
            {
                var userDb = await furniture.Users.FirstOrDefaultAsync(e => e.UserName.Equals(user.UserName));
                if (userDb != null)
                    return null;
                await furniture.Users.AddAsync(user);
                await furniture.SaveChangesAsync();
                return user;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<User> Login(User user)
        {

            User founsedUser = await furniture.Users.FirstOrDefaultAsync(userf => userf.UserName.Equals(user.UserName) && userf.Password.Equals(user.Password));
            if (founsedUser != null)
            {

                return founsedUser;

            }
            else
                return null;
        }
        public async Task<User> Update(int id, User user)
        {
            try { 
            List<User> users = await furniture.Users.ToListAsync();
            User u = await furniture.Users.FirstOrDefaultAsync(userf => userf.UserId.Equals(id));
            if (u != null)
            {
                u.UserName = user.UserName;
                u.FirstName = user.FirstName;
                u.LastName = user.LastName;
                u.Password = user.Password;
                u.Email = user.Email;
                await furniture.SaveChangesAsync();
                return u;
            }
            else
            {
                return null;
            }}
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
