
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
    public class CategoryRepositary : ICategoryRepositary
    {
        AdoNetOurStore326035854Context furniture = new AdoNetOurStore326035854Context();
        public CategoryRepositary(AdoNetOurStore326035854Context furniture)
        {
            this.furniture = furniture;

        }
        public async Task<List<Catgory>> Get()
        {
            var catgories = await furniture.Catgories.ToListAsync();
            if (catgories != null)
                return catgories;
            else return null;
        }
    }
}
