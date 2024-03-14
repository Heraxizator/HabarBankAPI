using HabarBankAPI.Domain.Share;
using System.ComponentModel.DataAnnotations;

namespace HabarBankAPI.Domain.Entities.ValutaBill
{
    public class ValutaScoreVariant : Entity
    {
        public ValutaScoreVariant() { }

        public ValutaScoreVariant(string name, string text, Valuta? valuta, int percentage, UserLevel? accountLevel, bool enabled)
        {
            this.Name = name;
            this.Text = text;
            this.Valuta = valuta;
            this.Percentage = percentage;
            this.AccountLevel = accountLevel;
            this.Enabled = enabled;
        }

        [Key]
        public long ValutaScoreVariantId { get; private set; }
        public string? Name { get; private set; }
        public string? Text { get; private set; }
        public Valuta? Valuta { get; private set; }
        public int Percentage { get; private set; }
        public UserLevel? AccountLevel { get; private set; }
    }
}
