using HabarBankAPI.Domain.Share;
using System.ComponentModel.DataAnnotations;

namespace HabarBankAPI.Domain.Entities.Card
{
    public class CardType : Entity
    {
        public CardType() { }

        public CardType(string name, bool enabled)
        {
            this.Name = name;
            this.Enabled = enabled;
        }

        [Key]
        public long CardTypeId { get; private set; }
        public string Name { get; private set; }

    }
}
