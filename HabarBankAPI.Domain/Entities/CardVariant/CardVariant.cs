using HabarBankAPI.Domain.Share;
using System.ComponentModel.DataAnnotations;

namespace HabarBankAPI.Domain.Entities
{
    public class CardVariant : Entity, IAggregateRoot
    {
        public CardVariant() { }
        public CardVariant(string name, string text, int percentage, CardType? cardType, UserLevel? accountLevel)
        {
            this.Name = name;
            this.Text = text;
            this.Percentage = percentage;
            this.CardType = cardType;
            this.AccountLevel = accountLevel;
        }

        [Key]
        public long CardVariantId { get; private set; }
        public string? Name { get; private set; }
        public string? Text { get; private set; }
        public int Percentage { get; private set; }

        public CardType CardType { get; private init; }
        public UserLevel AccountLevel { get; private init; }
        public ICollection<Card> Cards { get; }

    }
}
