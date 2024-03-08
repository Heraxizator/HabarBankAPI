
using HabarBankAPI.Domain.Share;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HabarBankAPI.Domain.Entities
{
    public abstract class Account : Entity, IAggregateRoot
    {
        public Account() { }

        public Account(string AccountLogin, string AccountPassword,
            string AccountPhone, string AccountName, string AccountSurname,
            string AccountPatronymic, bool AccountEnabled)
        {
            this.AccountLogin = AccountLogin;
            this.AccountPassword = AccountPassword;
            this.AccountPhone = AccountPhone;
            this.AccountName = AccountName;
            this.AccountSurname = AccountSurname;
            this.AccountPatronymic = AccountPatronymic;
            this.Enabled = AccountEnabled;
        }

        [Key]
        public long AccountId { get; protected set; }
        public string AccountLogin { get; protected set; }
        public string AccountPassword { get; protected set; }
        public string AccountPhone { get; protected set; }
        public string AccountName { get; protected set; }
        public string AccountSurname { get; protected set; }
        public string AccountPatronymic { get; protected set; }

        public void SetAccountProfile(string login, string password,  string phone,
            string name, string surname, string patronymic)
        {
            this.AccountLogin = login;
            this.AccountPassword = password;
            this.AccountPhone = phone;
            this.AccountName = name;
            this.AccountSurname = surname;
            this.AccountPatronymic = patronymic;
        }
    }
}
