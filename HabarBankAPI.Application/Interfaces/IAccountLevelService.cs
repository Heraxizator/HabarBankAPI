using HabarBankAPI.Application.DTO.AccountLevels;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.Interfaces
{
    public interface IAccountLevelService : IAppService
    {
        public Task CreateNewAccountLevel(AccountLevelDTO accountLevelDTO);
        public Task<IList<AccountLevelDTO>> GetAllAccountLevels();
        public Task<AccountLevelDTO> GetAccountLevelById(long id);
        public Task SetAccountLevelEnabled(long levelId,  bool levelEnabled);
    }
}
