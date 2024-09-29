using Entities;

namespace Repository
{
    public interface IRatingRepository
    {
        public int AddRating(Rating rating);
    }
}