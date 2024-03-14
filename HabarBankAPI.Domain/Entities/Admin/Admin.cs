using HabarBankAPI.Domain.Share;
using System.ComponentModel.DataAnnotations;

namespace HabarBankAPI.Domain.Entities.Admin
{
    public class Admin : Account, IAggregateRoot
    {
        public Admin() { }
        public Admin(string accountLogin, string accountPassword, string accountPhone,
            string accountName, string accountSurname, string accountPatronymic, bool accountEnabled) 
        {
            this.AccountLogin = accountLogin;
            this.AccountPassword = accountPassword;
            this.AccountPhone = accountPhone;
            this.AccountName = accountName;
            this.AccountSurname = accountSurname;
            this.AccountPatronymic = accountPatronymic;
            this.Enabled = accountEnabled;
        }

        [Key]
        public long AdminId { get; private init; }
    }
}
