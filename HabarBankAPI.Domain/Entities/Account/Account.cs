
using HabarBankAPI.Domain.Share;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HabarBankAPI.Domain.Specifications.Account;
using HabarBankAPI.Domain.Exceptions.Account;

namespace HabarBankAPI.Domain.Entities
{
    public abstract class Account : Entity
    {
        public Account() { }

        public string? AccountLogin { get; protected set; }
        public string? AccountPassword { get; protected set; }
        public string? AccountPhone { get; protected set; }
        public string? AccountName { get; protected set; }
        public string? AccountSurname { get; protected set; }
        public string? AccountPatronymic { get; protected set; }

        public void SetAccountProfile(string login, string password, string phone,
            string name, string surname, string patronymic)
        {
            LoginSpecification loginSpecification = new();

            if (loginSpecification.IsSatisfiedBy(login) is false)
            {
                throw new BadLoginException("Формат логина не является допустимым");
            }

            PasswordSpecification passwordSpecification = new();

            if (passwordSpecification.IsSatisfiedBy(password) is false)
            {
                throw new BadPasswordException("Пароль должен содержать не менее 6 символов");
            }

            PhoneSpecification phoneSpecification = new();

            if (phoneSpecification.IsSatisfiedBy(phone) is false)
            {
                throw new BadPhoneException("Номер должен содержать знак + и 11 цифр");
            }

            NameSpecification nameSpecification = new();

            if (nameSpecification.IsSatisfiedBy(name) is false)
            {
                throw new BadNameException("Имя не является реальным");
            }

            SurnameSpecification surnameSpecification = new();

            if (surnameSpecification.IsSatisfiedBy(surname) is false)
            {
                throw new BadSurnameException("Фамилия не является реальным");
            }

            PatronymicSpecification patronymicSpecification = new();

            if (patronymicSpecification.IsSatisfiedBy(patronymic) is false)
            {
                throw new BadPatronymicException("Отчество не является реальным");
            }

            this.AccountLogin = login;
            this.AccountPassword = password;
            this.AccountPhone = phone;
            this.AccountName = name;
            this.AccountSurname = surname;
            this.AccountPatronymic = patronymic;
        }
    }
}
