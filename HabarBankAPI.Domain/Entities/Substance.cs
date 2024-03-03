
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities
{
    public abstract class Substance : Entity
    {
        [Key]
        public int SubstanceId { get; protected set; }
        public int AccountId { get; private init; }
        public bool SubstanceEnabled { get; set; }
        public int RublesCount { get; set; }

        public void SetSubstanceEnabled(bool enabled)
        {
            this.SubstanceEnabled = enabled;
        }
    }
}
