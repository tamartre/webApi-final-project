using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Repositary;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tamar_Sheva_Project;

namespace TestProject
{
    public class UserUnitTest
    {
        [Fact]
        public async Task Login_ExistingUser_ReturnsUser()
        {
            // Arrange
            var mockUser = new User { UserName = "testUser", Password = "testPassword" };
            var users = new List<User> { mockUser };

            var mockContext = new Mock<AdoNetOurStore326035854Context>();
            mockContext.Setup(c => c.Users).ReturnsDbSet(users);

            var service = new UserRepositary(mockContext.Object);

            // Act
            var result = await service.Login(mockUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockUser, result);
        }

        [Fact]
        public async Task Login_NonExistentUser_ReturnsNull()
        {
            // Arrange
            var mockUser = new User { UserName = "testUser", Password = "testPassword" }; // Existing user
            var nonExistentUser = new User { UserName = "nonExistentUser", Password = "123456" }; // Non-existent user

            var mockContext = new Mock<AdoNetOurStore326035854Context>();
            var users = new List<User> { mockUser };
            mockContext.Setup(c => c.Users).ReturnsDbSet(users);

            var service = new UserRepositary(mockContext.Object);


            // Act
            var result = await service.Login(nonExistentUser);

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task Register_NullUser_ReturnsNoContentResult()
        {


            var userContextMock = new Mock<AdoNetOurStore326035854Context>();
            var user = new User { UserName = "testuser", Password = "password123" };
            userContextMock.Setup(x => x.Users.AddAsync(It.IsAny<User>(), default)).ThrowsAsync(new Exception("Simulated exception"));

            var userRepository = new UserRepositary(userContextMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(async () => await userRepository.Register(user));
        }

        [Fact]
        public async Task Register_new_User()
        {
            // Arrange
            var mockDbContext = new Mock<AdoNetOurStore326035854Context>();

            var user = new User { UserName = "newuser", Password = "password123", FirstName = "aaa", LastName = "aaa" };
            var userReg = new User { UserName = "aaa", Password = "aaa", FirstName = "aaa", LastName = "aaa" };
            mockDbContext.Setup(m => m.Users).ReturnsDbSet(new List<User> { user });
            var service = new UserRepositary(mockDbContext.Object);

            // Act
            var result = await service.Register(userReg);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userReg.UserName, result.UserName);
        }

        [Fact]
        public async Task TestRegister_ExceptionThrown_ReturnsNull()
        {
            // Arrange
            var mockDbContext = new Mock<AdoNetOurStore326035854Context>();
            var user = new User { UserName = "aaa", Password = "aaa", FirstName = "aaa", LastName = "aaa" };
            var userReg = new User { UserName = "aaa", Password = "aaa", FirstName = "aaa", LastName = "aaa" };
            mockDbContext.Setup(m => m.Users).ReturnsDbSet(new List<User> { user });
            var service = new UserRepositary(mockDbContext.Object);

            // Act
            var result = await service.Register(userReg);
            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Update_ExistingUser_SuccessfullyUpdated()
        {
            // Arrange
            var id = 1;
            var existingUser = new User { UserId = id, UserName = "existinguser", Password = "password123" };
            var updatedUser = new User { UserId = id, UserName = "updateduser", Password = "updated123" };

            var dbContextMock = new Mock<AdoNetOurStore326035854Context>();
            dbContextMock.Setup(m => m.Users).ReturnsDbSet(new List<User> { existingUser });

            var userRepository = new UserRepositary(dbContextMock.Object);

            // Act;
            var result = await userRepository.Update(id, updatedUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedUser.UserName, result.UserName);
            Assert.Equal(updatedUser.Password, result.Password);
        }
        [Fact]

        public async Task Update_NonExistingUser_ReturnsNull()
        {
            // Arrange
            var id = 1;
            var nonExistingUser = new User { UserId = id + 1, UserName = "nonexistinguser", Password = "password123" };
            var existingUser = new User { UserId = id, UserName = "existinguser", Password = "password123" };
            var dbContextMock = new Mock<AdoNetOurStore326035854Context>();
             dbContextMock.Setup(m => m.Users).ReturnsDbSet(new List<User>{existingUser});

            var userRepository = new UserRepositary(dbContextMock.Object);

            // Act
            var result = await userRepository.Update(id+1, nonExistingUser);

            // Assert
            Assert.Null(result);
        }

    }
}







