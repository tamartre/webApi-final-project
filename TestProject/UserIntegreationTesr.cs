using Entities;
using Repositary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamar_Sheva_Project;

namespace TestProject
{
    public class UserIntegreationTesr : IClassFixture<DatabaseFixture>
    {
        private readonly AdoNetOurStore326035854Context _dbontext;
        private readonly UserRepositary userRepositary;

        public UserIntegreationTesr(DatabaseFixture databaseFixture)
        {
            _dbontext = databaseFixture.Context;
            userRepositary = new UserRepositary(_dbontext);

        }
        [Fact]
        public async Task GetUser_Valid_returnsUser()
        {
            var email = "sg@gmail.com";
            var password = "a123s123";
            var user = new User { Email = email, Password = password, FirstName = "SHEVA", LastName = "KAPLAN", UserName = "koji" };
            await _dbontext.Users.AddAsync(user);
            await _dbontext.SaveChangesAsync();

            //act
            var result = await userRepositary.Login(user);

            //assert
            Assert.NotNull(result);
        }


    }
}
