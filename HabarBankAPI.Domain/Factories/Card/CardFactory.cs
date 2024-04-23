using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Exceptions.Account;
using HabarBankAPI.Domain.Exceptions.Card;
using HabarBankAPI.Domain.Specifications.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Factories
{
    public class CardFactory : ICardFactory
    {
        private string imagePath = string.Empty;
        private int rublesCount = default;
        private CardVariant? cardVariant = default;
        private User? user = default;

        public Card Build()
        {
            ImagePathSpecification imagePathSpecification = new();

            if (imagePathSpecification.IsSatisfiedBy(this.imagePath) is false)
            {
                throw new BadCardImagePathException("Формат ссылки для изображения некорректен");
            }

            RublesCountSpecification rublesCountSpecification = new();

            if (rublesCountSpecification.IsSatisfiedBy(this.rublesCount) is false)
            {
                throw new RublesCountArgumentException("Сумма в рублях должна быть больше ноля");
            }

            if (this.cardVariant is null)
            {
                throw new CardVariantNotFoundException("Вариант карты не найден");
            }
 
            if (this.user is null)
            {
                throw new AccountNotFoundException("Пользователь карты не найден");
            }

            Card card = new
            (
                this.cardVariant,
                this.rublesCount,
                this.imagePath, 
                this.user
            );

            card.SetEnabled(true);
            card.GenerateCardNumber("220220", 16);

            return card;
        }

        public ICardFactory WithCardUser(User? user)
        {
            this.user = user;
            return this;
        }

        public ICardFactory WithCardVariant(CardVariant? cardVariant)
        {
            this.cardVariant = cardVariant;
            return this;
        }

        public ICardFactory WithImagePath(string imagePath)
        {
            this.imagePath = imagePath;
            return this;
        }

        public ICardFactory WithRublesCount(int rublesCount)
        {
            this.rublesCount = rublesCount;
            return this;
        }
    }
}
