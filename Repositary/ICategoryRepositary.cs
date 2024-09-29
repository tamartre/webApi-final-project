
using Entities;
using Tamar_Sheva_Project;

namespace Repository
{
    public interface ICategoryRepositary
    {
        Task<List<Catgory>> Get();
    }
}