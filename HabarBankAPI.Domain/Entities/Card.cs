
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities
{
    public class Card : Substance
    {
        public Card() { }

        public Card(int cardId, int rublesCount, bool cardEnabled)
        {
            this.SubstanceId = cardId;
            //this.CardVariantId = cardVariantId;
            this.RublesCount = rublesCount;
            this.Enabled = cardEnabled;
        }

        public int CardVariantId { get; private init; }
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
