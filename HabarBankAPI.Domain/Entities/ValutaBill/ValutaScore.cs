using HabarBankAPI.Domain.Entities.Substance;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities.ValutaBill
{
    public class ValutaScore : Substance.Substance, IAggregateRoot
    {
        public ValutaScore() { }

        public long ValutaScoreId { get; private set; }
        public long ValutaScoreVariantId { get; private set; }
    }
}
