using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities
{
    public class ValutaScore : Substance
    {
        public ValutaScore() { }

        public int ValutaScoreId { get; private set; }
        public int ValutaScoreVariantId { get; private set; }
        public int RublesCount { get; private set; }
    }
}
