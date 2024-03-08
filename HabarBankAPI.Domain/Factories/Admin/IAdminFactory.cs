using HabarBankAPI.Domain.Entities.Admin;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain
{
    public interface IAdminFactory : IFactory<Admin>
    {
        IAdminFactory WithLogin(string login);
        IAdminFactory WithPassword(string password);
        IAdminFactory WithPhone(string phoneNumber);
        IAdminFactory WithName(string name);
        IAdminFactory WithSurname(string surname);
        IAdminFactory WithPatronymic(string patronymic);
    }
}
