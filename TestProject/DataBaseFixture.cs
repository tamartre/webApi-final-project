using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamar_Sheva_Project;

namespace TestProject
{
    public class DatabaseFixture : IDisposable
    {
        public AdoNetOurStore326035854Context Context { get; private set; }

        public DatabaseFixture()
        {
            // Set up the test database connection and initialize the context
            var options = new DbContextOptionsBuilder<AdoNetOurStore326035854Context>()
                .UseSqlServer("Server=srv2\\pupils;Database=Test_214345779;Trusted_Connection=True;TrustServerCertificate=True;")
                .Options;
            Context = new AdoNetOurStore326035854Context(options);
            Context.Database.EnsureCreated();// create the data base
        }

        public void Dispose()
        {
            // Clean up the test database after all tests are completed
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
