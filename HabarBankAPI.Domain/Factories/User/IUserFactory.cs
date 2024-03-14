using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Factories
{
    public interface IUserFactory : IFactory<User>
    {
        IUserFactory WithLogin(string login);
        IUserFactory WithPassword(string password);
        IUserFactory WithPhone(string phoneNumber);
        IUserFactory WithName(string name);
        IUserFactory WithSurname(string surname);
        IUserFactory WithPatronymic(string patronymic);
        IUserFactory WithAccountLevel(UserLevel? accountLevel);
    }
}
