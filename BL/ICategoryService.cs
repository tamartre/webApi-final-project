

using Entities;
using Tamar_Sheva_Project;

namespace Service
{
    public interface ICategoryService
    {
         Task<List<Catgory>> Get();
    }
}