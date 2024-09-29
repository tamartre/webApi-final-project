using Entities;
using Repositary;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public  class RatingService:IRatingService
    {
        IRatingRepository _repositary;
        public RatingService(IRatingRepository rating)
        {
            this._repositary = rating;
        }
        public async Task<int> AddRating(Rating rating)
        {
            int reasult =  _repositary.AddRating(rating);
            if (reasult == null)
                return 0;
            return reasult;


        }

    }
}
