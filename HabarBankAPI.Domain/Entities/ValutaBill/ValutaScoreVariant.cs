using HabarBankAPI.Domain.Share;
using System.ComponentModel.DataAnnotations;

namespace HabarBankAPI.Domain.Entities.ValutaBill
{
    public class ValutaScoreVariant : Entity
    {
        public ValutaScoreVariant() { }

        public ValutaScoreVariant(string name, string text, Valuta valuta, int percentage, long accountLevelId, bool enabled)
        {
            this.Name = name;
            this.Text = text;
            this.Valuta = valuta;
            this.Percentage = percentage;
            this.AccountLevelId = accountLevelId;
            this.Enabled = enabled;
        }

        [Key]
        public long ValutaScorevariantId { get; private set; }
        public string Name { get; private set; }
        public string Text { get; private set; }
        public Valuta Valuta { get; private set; }
        public int Percentage { get; private set; }
        public long AccountLevelId { get; private set; }
        public new bool Enabled { get; private set; }
    }
}
