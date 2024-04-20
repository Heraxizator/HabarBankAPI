using HabarBankAPI.Domain.Exceptions.Card;
using HabarBankAPI.Domain.Share;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabarBankAPI.Domain.Entities
{
    public class Card : Substance, IAggregateRoot
    {
        public Card() { }
        public Card(CardVariant? cardVariant, int rublesCount, string imagePath, User? user)
        {
            this.CardVariant = cardVariant;
            this.RublesCount = rublesCount;
            this.ImagePath = imagePath;
            this.User = user;
        }

        [Key]
        public long CardId { get; private init; }
        public string? CardNumber { get; private set; }
        public string? ImagePath { get; private set; }
        public CardVariant? CardVariant { get; private init; }

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
            if (rublesCount <= 0)
            {
                throw new RublesCountArgumentException("Нельзя начислить ноль и меньше рублей");
            }

            this.RublesCount += rublesCount;
        }

        public void DecreaseRublesCount(int rublesCount)
        {
            if (rublesCount <= 0)
            {
                throw new RublesCountArgumentException("Нельзя списать ноль и меньше рублей");
            }

            this.RublesCount -= rublesCount;
        }

        public void GenerateCardNumber(int length)
        {
            Random random = new();

            int[] digits;

            do
            {
                long number = random.NextInt64((long)Math.Pow(10, length - 1), (long)Math.Pow(10, length) - 1);

                digits = number.ToString().ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();

                for (int i = 0; i < digits.Length; i += 2)
                {
                    digits[i] *= 2;

                    if (digits[i] > 9)
                    {
                        digits[i] -= 9;
                    }
                }
            }

            while (digits.Sum() % 10 == 0);

            List<char> chars = digits.Select(x => char.Parse(x.ToString())).ToList();

            for (int i = 0; i < chars.Count; i += 5)
            {
                if (i == 0)
                {
                    continue;
                }

                chars.Insert(i - 1, '-');
            }

            this.CardNumber = string.Join(string.Empty, chars);
        }
    }
}
