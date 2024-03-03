using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabarBankAPI.Data.Entities
{
    [Table("card_type")]
    public class CardType
    {
        [Column("cardtype_id"), Key]
        public int CardTypeId { get; set; }

        [Column("cardtype_name")]
        public string CardTypeName { get; set; }

        [Column("cardtype_enabled")]
        public bool CardTypeEnabled { get; set; }

        public ICollection<CardVariant> CardVariants { get; set; }
    }
}
