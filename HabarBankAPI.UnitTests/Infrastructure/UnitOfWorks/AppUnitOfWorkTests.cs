using HabarBankAPI.Data;
using HabarBankAPI.Infrastructure.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.UnitTests.Infrastructure.UnitOfWorks
{
    public class AppUnitOfWorkTests
    {
        private AppUnitOfWork AppUnitOfOfWork;

        public AppUnitOfWorkTests() 
        {
            
        }

        [Fact]
        public async void CommitMethodTest()
        {
            this.AppUnitOfOfWork = new AppUnitOfWork(new ApplicationDbContext());

            Exception exception = await Record.ExceptionAsync(
                async () => await this.AppUnitOfOfWork.Commit());

            Assert.True(exception is null);
        }

        [Fact]
        public async void RollbackMethodTest()
        {
            this.AppUnitOfOfWork = new AppUnitOfWork(new ApplicationDbContext());

            Exception exception = await Record.ExceptionAsync(
                async () => await this.AppUnitOfOfWork.Rollback());

            Assert.True(exception is null);
        }

        [Fact]

        public async void DisposeMethodTest()
        {
            this.AppUnitOfOfWork = new AppUnitOfWork(new ApplicationDbContext());

            Exception exception = await Record.ExceptionAsync(
                async () => this.AppUnitOfOfWork.Dispose());

            Assert.True(exception is null);
        }
    }
}
