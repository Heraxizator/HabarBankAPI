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
        Task CreateAccount(UserDTO userDTO);
        Task<UserDTO> GetAccountById(int id);
        Task<UserDTO> GetAuthTokenByData(string login, string password);
        Task<UserDTO> GetAuthTokenBySMS(string phone, string sms);
        Task<IList<UserDTO>> GetAccountsList(int count);
        Task EditAccountEnabled(int id, bool enabled);
        Task EditAccountStatus(int id, AccountLevel level);
        Task EditAccountProfile(int id, ProfileDTO profileDTO);
    }
}
