using HabarBankAPI.Data;
using HabarBankAPI.Infrastructure.Share;
using HabarBankAPI.Infrastructure.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.UnitTests.Infrastructure.UnitOfWorks
{
    public class SecurityUnitOfWorkTests
    {
        private SecurityUnitOfWork SecurityUnitOfOfWork;

        public SecurityUnitOfWorkTests()
        {

        }

        [Fact]
        public async void CommitMethodTest()
        {
            this.SecurityUnitOfOfWork = new SecurityUnitOfWork(new SecurityDbContext());

            Exception exception = await Record.ExceptionAsync(
                async () => await this.SecurityUnitOfOfWork.Commit());

            Assert.True(exception is null);
        }

        [Fact]
        public async void RollbackMethodTest()
        {
            this.SecurityUnitOfOfWork = new SecurityUnitOfWork(new SecurityDbContext());

            Exception exception = await Record.ExceptionAsync(
                async () => await this.SecurityUnitOfOfWork.Rollback());

            Assert.True(exception is null);
        }

        [Fact]

        public async void DisposeMethodTest()
        {
            this.SecurityUnitOfOfWork = new SecurityUnitOfWork(new SecurityDbContext());

            Exception exception = await Record.ExceptionAsync(
                async () => this.SecurityUnitOfOfWork.Dispose());

            Assert.True(exception is null);
        }
    }
}
