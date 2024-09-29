
using Repositary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Tamar_Sheva_Project;
using Entities;
namespace Service
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepositary _repositary;
        public CategoryService(ICategoryRepositary categoryService)
        {
            this._repositary = categoryService;
        }
        public async Task<List<Catgory>> Get()
        {
            List <Catgory> reasult = await _repositary.Get();
            if (reasult == null)
                return null;
            return reasult;
        }

    }
}
