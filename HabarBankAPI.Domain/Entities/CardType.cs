
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities
{
    public class CardType : Entity
    {
        public CardType() { }

        public CardType(string name, bool enabled)
        {
            this.Name = name;
            this.Enabled = enabled;
        }

        [Key]
        public int CardTypeId { get; private set; }
        public string Name { get; private set; }

    }
}
