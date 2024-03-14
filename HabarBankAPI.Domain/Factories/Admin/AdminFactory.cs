using HabarBankAPI.Domain.Entities.Admin;
using HabarBankAPI.Domain.Exceptions.Account;
using HabarBankAPI.Domain.Factories;
using HabarBankAPI.Domain.Share;
using HabarBankAPI.Domain.Specifications.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain
{
    public class AdminFactory : IAdminFactory
    {
        private string accountLogin = string.Empty;
        private string accountPassword = string.Empty;
        private string accountPhone = string.Empty;
        private string accountName = string.Empty;
        private string accountSurname = string.Empty;
        private string accountPatronymic = string.Empty;

        public Admin Build()
        {
            Admin admin = new();

            admin.SetAccountProfile
            (
                this.accountLogin,
                this.accountPassword,
                this.accountPhone,
                this.accountName,
                this.accountSurname,
                this.accountPatronymic
            );

            admin.SetEnabled(true);

            return admin;
        }

        public IAdminFactory WithLogin(string login)
        {
            this.accountLogin = login;
            return this;
        }

        public IAdminFactory WithName(string name)
        {
            this.accountName = name;
            return this;
        }

        public IAdminFactory WithPassword(string password)
        {
            this.accountPassword = password;
            return this;
        }

        public IAdminFactory WithPatronymic(string patronymic)
        {
            this.accountPatronymic = patronymic;
            return this;
        }

        public IAdminFactory WithPhone(string phoneNumber)
        {
            this.accountPhone = phoneNumber;
            return this;
        }

        public IAdminFactory WithSurname(string surname)
        {
            this.accountSurname = surname;
            return this;
        }
    }
}
