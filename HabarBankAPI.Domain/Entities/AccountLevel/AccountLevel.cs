using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities.AccountLevel
{
    public class AccountLevel : Entity, IAggregateRoot
    {
        public AccountLevel() { }

        public AccountLevel(string name)
        {
            Name = name;
            Enabled = true;
        }

        [Key]
        public long AccountLevelId { get; set; }
        public string Name { get; set; }

        public void SetAccountLevelName(string name)
        {
            Name = name;
        }
    }
}
