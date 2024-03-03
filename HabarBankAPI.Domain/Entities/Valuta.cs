
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities
{
    public class Valuta : Entity
    {
        public Valuta() { }

        public Valuta(int valutaId, string valutaName, int valutaRubles, bool valutaEnabled)
        {
            this.ValutaId = valutaId;
            this.ValutaName = valutaName;
            this.ValutaRubles = valutaRubles;
            this.ValutaEnabled = valutaEnabled;
        }

        [Key]
        public int ValutaId { get; private init; }
        public string ValutaName { get; private set;}
        public int ValutaRubles { get; private set;}
        public bool ValutaEnabled { get; private set;}

    }
}
