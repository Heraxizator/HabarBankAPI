using HabarBankAPI.Domain.Share;
using System.ComponentModel.DataAnnotations;

namespace HabarBankAPI.Domain.Entities
{
    public class CardType : Entity
    {
        public CardType() { }
        public CardType(string name, bool enabled)
        {
            Name = name;
            Enabled = enabled;
        }

        [Key]
        public long CardTypeId { get; private set; }
        public string? Name { get; private set; }

        public ICollection<CardVariant> CardVariants { get; }
    }
}
