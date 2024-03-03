
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities
{
    public class AccountLevel : Entity
    {
        public AccountLevel() { }

        public AccountLevel(string name, bool enabled)
        {
            this.Name = name;
            this.Enabled = enabled;

        }

        [Key]
        public int AccountLevelId { get; set; }
        public string Name { get; set; }

        public void SetAccountLevelName(string name)
        {
            this.Name = name;
        }
    }
}
