using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities.ValutaBill
{
    public class ValutaScore : Substance, IAggregateRoot
    {
        public ValutaScore() { }

        [Key]
        public long ValutaScoreId { get; private set; }
        public ValutaScoreVariant? ValutaScoreVariant { get; private set; }
    }
}
