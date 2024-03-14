using HabarBankAPI.Application.DTO;
using HabarBankAPI.Application.DTO.Account;
using HabarBankAPI.Application.DTO.Accounts;
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
    public interface IUserService : IAppService
    {
        Task CreateUserAccount(UserDTO userDTO);
        Task<UserDTO> GetAccountById(long id);
        Task<UserDTO> GetAuthTokenByData(string login, string password);
        Task<UserDTO> GetAuthTokenBySMS(string phone, string sms);
        Task<IList<UserDTO>> GetAccountsList(int count);
        Task EditAccountEnabled(long id, bool enabled);
        Task EditAccountStatus(long id, UserLevel level);
        Task EditAccountProfile(long id, ProfileDTO profileDTO);
    }
}
