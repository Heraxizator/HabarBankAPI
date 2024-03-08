using HabarBankAPI.Domain.Share;
using System.ComponentModel.DataAnnotations;

namespace HabarBankAPI.Domain.Entities.ValutaBill
{
    public class Valuta : Entity
    {
        public Valuta() { }

        public Valuta(long valutaId, string valutaName, int valutaRubles, bool valutaEnabled)
        {
            this.ValutaId = valutaId;
            this.ValutaName = valutaName;
            this.ValutaRubles = valutaRubles;
            this.ValutaEnabled = valutaEnabled;
        }

        [Key]
        public long ValutaId { get; private init; }
        public string ValutaName { get; private set; }
        public int ValutaRubles { get; private set; }
        public bool ValutaEnabled { get; private set; }

    }
}
