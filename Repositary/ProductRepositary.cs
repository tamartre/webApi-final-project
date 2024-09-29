
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
    public class ProductRepositary : IProductRepositary
    {
        AdoNetOurStore326035854Context furniture = new AdoNetOurStore326035854Context();
        public ProductRepositary(AdoNetOurStore326035854Context furniture)
        {
            this.furniture = furniture;

        }
        public async Task<List<Product>> Get(string? descreption, int? min, int? max, string? name, int?[] categoryIds, int position, int skip )
        {
            var query =  furniture.Products.Include(p => p.Category).Where(product =>
            (descreption == null ? (true) : (product.Description.Contains(descreption)))
            && ((min == null) ? (true) : (product.Price >= min))
            && ((max == null) ? (true) : (product.Price <= max))
            && ((categoryIds.Length == 0) ? (true) : (categoryIds.Contains((int)product.CategoryId))))
            .OrderBy(product => product.Price);
            Console.WriteLine(query.ToQueryString());
            List<Product> products = await query.ToListAsync();
             return products;
        }
     

    }
}
