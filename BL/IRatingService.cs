using Entities;

namespace Service
{
    public interface IRatingService
    {
        public Task<int> AddRating(Rating rating);
    }
}