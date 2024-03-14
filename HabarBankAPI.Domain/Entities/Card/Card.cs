using HabarBankAPI.Domain.Exceptions.Card;
using HabarBankAPI.Domain.Share;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabarBankAPI.Domain.Entities
{
    public class Card : Substance, IAggregateRoot
    {
        public Card() { }
        public Card(CardVariant? cardVariant, int rublesCount, string imagePath, User? user, bool cardEnabled)
        {
            this.CardVariant = cardVariant;
            this.RublesCount = rublesCount;
            this.ImagePath = imagePath;
            this.User = user;
            this.Enabled = cardEnabled;
        }

        [Key]
        public long CardId { get; private init; }
        public string? ImagePath { get; private set; }
        public CardVariant CardVariant { get; private init; }

        public void SetPercentages()
        {
            if (this.CardVariant is null)
            {
                throw new CardVariantNotFoundException("Вариант карты не может иметь значение null");
            }

            this.RublesCount += (int)(this.RublesCount * this.CardVariant.Percentage * 0.01);
        }

        public void IncreaseRublesCount(int rublesCount)
        {
            this.RublesCount += rublesCount;
        }

        public void DecreaseRublesCount(int rublesCount)
        {
            this.RublesCount -= rublesCount;
        }
    }
}
