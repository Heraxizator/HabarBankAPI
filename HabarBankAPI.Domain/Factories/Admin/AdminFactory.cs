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
            LoginSpecification loginSpecification = new();

            if (loginSpecification.IsSatisfiedBy(this.accountLogin) is false)
            {
                throw new BadLoginException("Логин содержит хотя бы одну букву и не менее 5 символов");
            }

            PasswordSpecification passwordSpecification = new();

            if (passwordSpecification.IsSatisfiedBy(this.accountPassword) is false)
            {
                throw new BadPasswordException("Длина пароля не менее 6 символов");
            }

            PhoneSpecification phoneSpecification = new();

            if (phoneSpecification.IsSatisfiedBy(this.accountPhone) is false)
            {
                throw new BadPhoneException("Длина номера ровно 12 символов, а также имеется знак +");
            }

            Specifications.Account.NameSpecification nameSpecification = new();

            if (nameSpecification.IsSatisfiedBy(this.accountName) is false)
            {
                throw new BadNameException("Имя пользователя может иметь только буквы");
            }

            SurnameSpecification surnameSpecification = new();

            if (surnameSpecification.IsSatisfiedBy(this.accountSurname) is false)
            {
                throw new BadSurnameException("Фамилия может иметь только буквы");
            }

            PatronymicSpecification patronymicSpecification = new();

            if (patronymicSpecification.IsSatisfiedBy(this.accountPatronymic) is false)
            {
                throw new BadPatronymicException("Отчество может иметь только буквы");
            }

            return new Admin
            (
                this.accountLogin,
                this.accountPassword,
                this.accountPhone,
                this.accountName,
                this.accountSurname,
                this.accountPatronymic,
                true
            );
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
