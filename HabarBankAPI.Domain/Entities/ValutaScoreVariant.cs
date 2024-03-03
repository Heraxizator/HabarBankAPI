
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities
{
    public class ValutaScoreVariant : Entity
    {
        public ValutaScoreVariant() { }

        public ValutaScoreVariant(string name, string text, Valuta valuta, int percentage, int accountLevelId, bool enabled)
        {
            this.Name = name;
            this.Text = text;
            this.Valuta = valuta;
            this.Percentage = percentage;
            this.AccountLevelId = accountLevelId;
            this.Enabled = enabled;
        }

        [Key]
        public int ValutaScorevariantId { get; private set; }
        public string Name { get; private set; }
        public string Text { get; private set; }
        public Valuta Valuta { get; private set; }
        public int Percentage { get; private set; }
        public int AccountLevelId { get; private set; }
        public bool Enabled { get; private set; }
    }
}
