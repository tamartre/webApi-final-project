using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Repositary;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamar_Sheva_Project;

namespace TestProject
{
    public class ProductUnitTest
    {


        //{
        //    // Arrange
        //    var mockUser = new  { UserName = "testUser", Password = "testPassword" };
        //    var users = new List<User> { mockUser };

        //    var mockContext = new Mock<AdoNetOurStore326035854Context>();
        //    mockContext.Setup(c => c.Users).ReturnsDbSet(users);

        //    var service = new UserRepositary(mockContext.Object);

        //    // Act
        //    var result = await service.Login(mockUser);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal(mockUser, result);

        [Fact]
        public async Task GetProducts_WithFilters_ReturnsCorrectProducts()
        {
            // Arrange

            var mockDbContext = new Mock<AdoNetOurStore326035854Context>();

            var categories = new List<Catgory>
        {
            new Catgory { CategoryId = 1, CategoryName = "Furniture" },
            new Catgory { CategoryId = 2, CategoryName = "Electronics" }
        };
            var products = new List<Product>
            {
                new Product { ProductId = 1,ProductName="ee", Description = "Modern sofa", Price = 300, CategoryId = 1, Category = categories[0] },
                new Product { ProductId = 2,ProductName="ed", Description = "Vintage chair", Price = 150, CategoryId = 1, Category = categories[0] },
                new Product { ProductId = 3, ProductName="es",Description = "Smart TV", Price = 600, CategoryId = 2, Category = categories[1] },
                new Product { ProductId = 4, ProductName="ex",Description = "Office desk", Price = 200, CategoryId = 1, Category = categories[0] }
            };

            // Setup the DbSet for Categories
            mockDbContext.Setup(m => m.Catgories).ReturnsDbSet(categories);
            mockDbContext.Setup(c => c.Products).ReturnsDbSet(products);

            var service = new ProductRepositary(mockDbContext.Object);

            // Act
            var result = await service.Get("sofa", 100, 400, null, new int?[] { 1 }, 0, 0);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(300, result.First().Price);
            Assert.Equal("Modern sofa", result.First().Description);
        }



    }
}