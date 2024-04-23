using HabarBankAPI.Domain.Exceptions.Card;
using HabarBankAPI.Domain.Share;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

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

        public void GenerateCardNumber(string prefix, int length)
        {
            Random random = new();

            int[] digits;

            long number = random.NextInt64((long)Math.Pow(10, length - 2 - prefix.Length), (long)Math.Pow(10, length - 1 - prefix.Length) - 1);

            digits = number.ToString().ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();

            for (int i = 0; i < digits.Length; i += 2)
            {
                digits[i] *= 2;

                if (digits[i] > 9)
                {
                    digits[i] -= 9;
                }
            }

            string result = "220220" + string.Join(string.Empty, digits.Select(x => char.Parse(x.ToString())));

            int sum = result.Reverse() 
                        .Select((d, i) => i % 2 == 0 ? Convert.ToInt32(d.ToString()) * 2 : Convert.ToInt32(d.ToString()))
                            .Select(x => x.ToString().Select(c => Convert.ToInt32(c.ToString())).Sum()) 
                              .Sum();

            int digit = (10 - (sum % 10)) % 10;

            this.CardNumber = Regex.Replace(result + digit, ".{4}", "$0 ").TrimEnd();
        }
    }
}
