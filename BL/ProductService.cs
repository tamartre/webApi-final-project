
using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamar_Sheva_Project;

namespace Service
{
    public class ProductService : IProductService
    {
        IProductRepositary _repositary;
        public ProductService(IProductRepositary productService)
        {
            this._repositary = productService;
        }
        public async Task<List<Product>> Get(string? descreption, int? min, int? max, string? name, int?[] categoryIds, int position = 20, int skip = 1)
        {
            List<Product> reasult = await _repositary.Get(descreption,min,max,name, categoryIds, position , skip );
            if (reasult == null)
                return null;
            return reasult;
        }
    }
}
