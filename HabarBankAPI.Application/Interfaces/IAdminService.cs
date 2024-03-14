using HabarBankAPI.Application.DTO.Accounts;
using HabarBankAPI.Application.DTO.Admins;
using HabarBankAPI.Application.DTO.Users;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.Interfaces
{
    public interface IAdminService : IAppService
    {
        Task CreateAdminAccount(AdminDTO adminDTO);
        Task<IList<AdminDTO>> GetAccountsList(int count);
        Task<AdminDTO> GetAccountById(long id);
        Task<AdminDTO> GetAuthTokenByData(string login, string password);
        Task EditAccountEnabled(long id, bool enabled);
        Task EditAccountProfile(long id, ProfileDTO profileDTO);
    }
}
