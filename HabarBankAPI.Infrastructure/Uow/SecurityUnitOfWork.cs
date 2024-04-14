using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Database;
using HabarBankAPI.Infrastructure.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Infrastructure.Uow
{
    public class SecurityUnitOfWork : IUnitOfWork
    {
        private readonly SecurityDbContext _context;

        public SecurityUnitOfWork(SecurityDbContext context)
        {
            this._context = context;
        }

        public async Task<bool> Commit()
        {
            bool success = (await this._context.SaveChangesAsync()) > 0;

            return success;
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        public Task Rollback()
        {
            return Task.CompletedTask;
        }
    }
}
