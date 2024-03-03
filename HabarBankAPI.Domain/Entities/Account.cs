
using HabarBankAPI.Domain.Share;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HabarBankAPI.Domain.Entities
{
    public class Account : Entity
    {
        public Account() { }

        public Account(int AccountId, string AccountLogin, string AccountPassword,
            string AccountPhone, string AccountName, string AccountSurname,
            string AccountPatronymic, int AccountLevelId, bool AccountEnabled)
        {
            this.AccountId = AccountId;
            this.AccountLogin = AccountLogin;
            this.AccountPassword = AccountPassword;
            this.AccountPhone = AccountPhone;
            this.AccountName = AccountName;
            this.AccountSurname = AccountSurname;
            this.AccountPatronymic = AccountPatronymic;
            this.AccountLevelId = AccountLevelId;
            this.Enabled = AccountEnabled;
        }

        [Key]
        public int AccountId { get; private set; }
        public string AccountLogin { get; private set; }
        public string AccountPassword { get; private set; }
        public string AccountPhone { get; private set; }
        public string AccountName { get; private set; }
        public string AccountSurname { get; private set; }
        public string AccountPatronymic { get; private set; }
        public int AccountLevelId { get; private set; }

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

        public void SetAccountStatus(int levelId)
        {
            this.AccountLevelId = levelId;
        }
    }
}
