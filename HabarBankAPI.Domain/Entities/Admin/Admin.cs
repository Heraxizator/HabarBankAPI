using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities.Admin
{
    public class Admin : Account, IAggregateRoot
    {
        public Admin() { }

        public Admin(string AccountLogin, string AccountPassword,
            string AccountPhone, string AccountName, string AccountSurname,
            string AccountPatronymic, bool AccountEnabled)
        {
            this.AccountLogin = AccountLogin;
            this.AccountPassword = AccountPassword;
            this.AccountPhone = AccountPhone;
            this.AccountName = AccountName;
            this.AccountSurname = AccountSurname;
            this.AccountPatronymic = AccountPatronymic;
            Enabled = AccountEnabled;
        }
    }
}
