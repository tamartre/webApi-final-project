using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Service;
using System.Threading.Tasks;

namespace Tamar_Sheva_Project
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MiddlewareRating
    {
        private readonly RequestDelegate _next;
        string connectionString =
     "Data Source = srv2\\PUPILS; Initial Catalog = Ado_Net_OurStore_326035854;Integrated Security = True;Pooling = False";

        public MiddlewareRating(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext , IRatingService ratingService)
        {

            Rating rating = new Rating();
            rating.Host = httpContext.Request.Host.Host;
            rating.Method = httpContext.Request.Method;
            rating.Path = httpContext.Request.Path;
            rating.Referer = httpContext.Request.Headers["Referer"];
            rating.UserAgent = httpContext.Request.Headers.UserAgent;

            ratingService.AddRating(rating);
            return _next(httpContext);
        }
    }

  
    public static class MiddlewareRatingExtensions
    {
        public static IApplicationBuilder UseMiddlewareRating(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MiddlewareRating>();
        }
    }

}
