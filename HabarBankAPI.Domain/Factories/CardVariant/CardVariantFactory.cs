using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Exceptions.AccountLevel;
using HabarBankAPI.Domain.Exceptions.Card;
using HabarBankAPI.Domain.Exceptions.CardVariant;
using HabarBankAPI.Domain.Specifications.CardVariant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Factories
{
    public class CardVariantFactory : ICardVariantFactory
    {
        private string _variantName = string.Empty;
        private string _variantText = string.Empty;
        private int _percentageCount = new();
        private UserLevel? _userLevel = default;
        private CardType? _cardType = default;

        public CardVariant Build()
        {
            CardVariantNameSpecification cardVariantNameSpecification = new();

            if (cardVariantNameSpecification.IsSatisfiedBy(this._variantName) is false)
            {
                throw new BadVariantNameException("Название варианта не соотвествует требованиям");
            }

            CardVariantTextSpecification cardVariantTextSpecification = new();

            if (cardVariantTextSpecification.IsSatisfiedBy(this._variantText) is false)
            {
                throw new BadVariantTextException("Текст варианта не соотвествует требованиям");
            }

            CardVariantPercentageSpecification cardVariantPercentageSpecification = new();

            if (cardVariantPercentageSpecification.IsSatisfiedBy(this._percentageCount) is false)
            {
                throw new BadVariantPercentageException("Процент должен быть не меньше нуля");
            }

            if (this._userLevel is null)
            {
                throw new AccountLevelNotFoundException("Уровень пользователя не найден");
            }

            if (this._cardType is null)
            {
                throw new CardTypeNotFoundException("Тип карты не найден");
            }

            CardVariant cardVariant = new
            (
                this._variantName,
                this._variantText,
                this._percentageCount,
                this._cardType,
                this._userLevel
            );

            cardVariant.SetEnabled(true); 
            
            return cardVariant;
        }

        public ICardVariantFactory WithCardType(CardType? cardType)
        {
            this._cardType = cardType;
            return this;
        }

        public ICardVariantFactory WithName(string name)
        {
            this._variantName = name;
            return this;
        }

        public ICardVariantFactory WithPercentage(int percentage)
        {
            this._percentageCount = percentage;
            return this;
        }

        public ICardVariantFactory WithText(string text)
        {
            this._variantText = text;
            return this;
        }

        public ICardVariantFactory WithUserLevel(UserLevel? userLevel)
        {
            this._userLevel = userLevel;
            return this;
        }
    }
}
