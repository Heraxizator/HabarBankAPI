using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabarBankAPI.Data.Entities
{
    [Table("cardvariant")]
    public class CardVariant
    {
        [Column("cardvariant_id"), Key]
        public int CardVariantId { get; set; }

        [Column("cardvariant_name")]
        public string CardVariantName { get; set; }

        [Column("cardvariant_text")]
        public string CardVariantText { get; set; }

        [Column("cardvariant_percentage")]
        public int CardVariantPercentage { get; set; }

        [Column("cardvariant_cardtype_id")]
        public int CardVariantCardTypeId { get; set; }

        [Column("cardvariant_userlevel_id")]
        public int CardVariantUserLevelId { get; set; }

        [Column("cardvariant_enabled")]
        public bool CardVariantEnabled { get; set; }

        public ICollection<Card> Cards { get; set; }
        public UserLevel UserLevel { get; set; }
        public CardType CardType { get; set; }
    }
}
