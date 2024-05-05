using HabarBankAPI.Data;
using HabarBankAPI.Infrastructure.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.UnitTests.Infrastructure.DbContexts
{
    public class SecurityDbContextTests
    {
        [Fact]
        public void CheckDbCanBeConnectedTest()
        {
            // Arrange

            SecurityDbContext context = new();

            // Action

            bool result = context.Database.CanConnect();

            // Assert 

            Assert.True(result);
        }

        [Fact]
        public void CheckDbExistTest()
        {
            // Arrange

            SecurityDbContext context = new();

            // Action

            bool result = context.Database.EnsureCreated();

            // Assert 

            Assert.True(result);
        }
    }
}
