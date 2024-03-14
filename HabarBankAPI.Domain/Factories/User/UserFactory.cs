using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Exceptions.Account;
using HabarBankAPI.Domain.Exceptions.AccountLevel;
using HabarBankAPI.Domain.Specifications.Account;
using HabarBankAPI.Domain.Specifications.AccountLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Factories
{
    public class UserFactory : IUserFactory
    {
        private string accountLogin = string.Empty;
        private string accountPassword = string.Empty;
        private string accountPhone = string.Empty;
        private string accountName = string.Empty;
        private string accountSurname = string.Empty;
        private string accountPatronymic = string.Empty;
        private UserLevel? accountLevel = default;

        public User Build()
        {
            User user = new();

            user.SetAccountProfile
            (
                this.accountLogin,
                this.accountPassword,
                this.accountPhone,
                this.accountName,
                this.accountSurname,
                this.accountPatronymic
            );

            user.SetUserStatus(this.accountLevel);

            user.SetEnabled(true);

            return user;
        }

        public IUserFactory WithAccountLevel(UserLevel? accountLevel)
        {
            this.accountLevel = accountLevel;
            return this;
        }

        public IUserFactory WithLogin(string login)
        {
            this.accountLogin = login;
            return this;
        }

        public IUserFactory WithName(string name)
        {
            this.accountName = name;
            return this;
        }

        public IUserFactory WithPassword(string password)
        {
            this.accountPassword = password;
            return this;
        }

        public IUserFactory WithPatronymic(string patronymic)
        {
            this.accountPatronymic = patronymic;
            return this;
        }

        public IUserFactory WithPhone(string phoneNumber)
        {
            this.accountPhone = phoneNumber;
            return this;
        }

        public IUserFactory WithSurname(string surname)
        {
            this.accountSurname = surname;
            return this;
        }
    }
}
