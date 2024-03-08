using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities.User
{
    public class User : Account, IAggregateRoot
    {
        public User() { }

        public User(string AccountLogin, string AccountPassword,
            string AccountPhone, string AccountName, string AccountSurname,
            string AccountPatronymic, long AccountLevelId, bool AccountEnabled)
        {
            this.AccountLogin = AccountLogin;
            this.AccountPassword = AccountPassword;
            this.AccountPhone = AccountPhone;
            this.AccountName = AccountName;
            this.AccountSurname = AccountSurname;
            this.AccountPatronymic = AccountPatronymic;
            this.AccountLevelId = AccountLevelId;
            Enabled = AccountEnabled;
        }

        public long AccountLevelId { get; private set; }

        public void SetUserStatus(long accountLevelId)
        {
            AccountLevelId = accountLevelId;
        }

    }
}
