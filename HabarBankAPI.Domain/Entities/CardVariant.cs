
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities
{
    public class CardVariant : Entity
    {
        public CardVariant() { }

        public CardVariant(string name, string text, int percentage, int cardTypeId, int accountLevelId)
        {
            this.Name = name;
            this.Text = text;
            this.Percentage = percentage;
            this.CardTypeId = cardTypeId;
            this.AccountLevelId = accountLevelId;
        }

        [Key]
        public int CardVariantId { get; private set; }
        public string Name { get; private set; }
        public string Text { get; private set; }
        public int Percentage { get; private set; }
        public int CardTypeId { get; private init; }
        public int AccountLevelId { get; private init; }

    }
}
