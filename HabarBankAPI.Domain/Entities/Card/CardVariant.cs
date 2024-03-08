using HabarBankAPI.Domain.Share;
using System.ComponentModel.DataAnnotations;

namespace HabarBankAPI.Domain.Entities.Card
{
    public class CardVariant : Entity
    {
        public CardVariant() { }

        public CardVariant(string name, string text, int percentage, long cardTypeId, long accountLevelId)
        {
            this.Name = name;
            this.Text = text;
            this.Percentage = percentage;
            this.CardTypeId = cardTypeId;
            this.AccountLevelId = accountLevelId;
        }

        [Key]
        public long CardVariantId { get; private set; }
        public string Name { get; private set; }
        public string Text { get; private set; }
        public int Percentage { get; private set; }
        public long CardTypeId { get; private init; }
        public long AccountLevelId { get; private init; }

    }
}
