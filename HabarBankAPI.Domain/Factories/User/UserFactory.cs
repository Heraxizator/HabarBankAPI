using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Entities.User;
using HabarBankAPI.Domain.Exceptions.Account;
using HabarBankAPI.Domain.Exceptions.AccountLevel;
using HabarBankAPI.Domain.Specifications.Account;
using HabarBankAPI.Domain.Specifications.AccountLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BadNameException = HabarBankAPI.Domain.Exceptions.Account.BadNameException;

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
        private long accountLevelId = default;

        public User Build()
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

            return new User
            (
                this.accountLogin,
                this.accountPassword,
                this.accountPhone,
                this.accountName,
                this.accountSurname,
                this.accountPatronymic,
                this.accountLevelId,
                true
            );
        }

        public IUserFactory WithAccountLevelId(long accountLevelId)
        {
            this.accountLevelId = accountLevelId;
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
