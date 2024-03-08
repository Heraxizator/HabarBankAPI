using HabarBankAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain
{
    public class Admin : Account
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
            this.Enabled = AccountEnabled;
        }
    }
}
