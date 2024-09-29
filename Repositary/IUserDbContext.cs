using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUserDbContext
    {
        Task<User> GetUserAsync(Func<User, bool> predicate);
    }
}
