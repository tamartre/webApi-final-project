using Microsoft.EntityFrameworkCore;
using NLog.Web;
using PresidentsApp.Middlewares;
using Repositary;
using Repository;
using Service;
using Tamar_Sheva_Project;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepositary, UserRepositary>();
builder.Services.AddScoped<ICategoryRepositary, CategoryRepositary>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepositary, ProductRepositary>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddDbContext<AdoNetOurStore326035854Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("School")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Configure the HTTP request pipeline.
builder.Host.UseNLog();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseErrorHandlingMiddleware();

app.UseMiddlewareRating();

app.MapControllers();

app.UseStaticFiles();

app.Run();
