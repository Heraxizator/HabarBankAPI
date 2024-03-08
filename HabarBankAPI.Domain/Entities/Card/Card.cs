using HabarBankAPI.Domain.Share;

namespace HabarBankAPI.Domain.Entities.Card
{
    public class Card : Substance.Substance, IAggregateRoot
    {
        public Card() { }

        public Card(long cardVariantId, int rublesCount, string imagePath, bool cardEnabled)
        {
            this.CardVariantId = cardVariantId;
            this.RublesCount = rublesCount;
            this.ImagePath = imagePath;
            this.Enabled = cardEnabled;
        }

        public long CardVariantId { get; private init; }
        public string ImagePath { get; private set; }

        public void SetPercentages(int percentage)
        {
            this.RublesCount += (int)(this.RublesCount * percentage * 0.01);
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
