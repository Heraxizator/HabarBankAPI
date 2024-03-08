using HabarBankAPI.Domain.Entities.Card;
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
        private long cardVariantId = default;

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

            return new Card
            (
                this.cardVariantId,
                this.rublesCount,
                this.imagePath, true
            );
        }

        public ICardFactory WithCardVariantId(long cardVariantId)
        {
            this.cardVariantId = cardVariantId;
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
