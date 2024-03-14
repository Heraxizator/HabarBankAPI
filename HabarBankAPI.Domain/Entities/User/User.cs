using HabarBankAPI.Domain.Entities.ValutaBill;
using HabarBankAPI.Domain.Exceptions.AccountLevel;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities
{
    public class User : Account, IAggregateRoot
    {
        public User() { }

        public User(string? userLogin, string? userPassword, string? userPhone,
            string? userName, string? userSurname, string? userPatronymic,
            UserLevel? userLevel, bool userEnabled) 
        {
            this.AccountLogin = userLogin;
            this.AccountPassword = userPassword;
            this.AccountPhone = userPhone;
            this.AccountName = userName;
            this.AccountSurname = userSurname;
            this.AccountPatronymic = userPatronymic;
            this.Enabled = userEnabled;
            this.UserLevel = userLevel;
        }

        [Key]
        public long UserId { get; private init; }
        public UserLevel UserLevel { get; private set; }
        public ICollection<Card> Cards { get; set; }
        public ICollection<MetalScore> MetalScores { get; set; }
        public ICollection<ValutaScore> ValutaScores { get; set; }

        public void SetUserStatus(UserLevel? userLevel)
        {
            if (userLevel is null)
            {
                throw new AccountLevelNotFoundException("Уровень аккаунта не может иметь значение null");
            }

            this.UserLevel = userLevel;
        }

    }
}
