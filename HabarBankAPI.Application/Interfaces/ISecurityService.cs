using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.Interfaces
{
    public interface ISecurityService : IAppService
    {
        public Task<string?> GetToken(string email);
        public Task GenerateToken(string email);
        public Task IsExists(string token);
    }
}
