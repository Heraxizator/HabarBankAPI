using HabarBankAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.UnitTests.Infrastructure.DbContexts
{
    public class ApplicationDbContextTests
    {
        [Fact]
        public void CheckDbCanBeConnectedTest()
        {
            // Arrange

            ApplicationDbContext context = new();

            // Action

            bool result = context.Database.CanConnect();

            // Assert 

            Assert.True(result);
        }

        [Fact]
        public void CheckDbExistTest()
        {
            // Arrange

            ApplicationDbContext context = new();

            // Action

            bool result = context.Database.EnsureCreated();

            // Assert 

            Assert.True(result);
        }
    }
}
