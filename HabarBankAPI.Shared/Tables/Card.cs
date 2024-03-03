using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabarBankAPI.Data.Entities
{
    [Table("card")]
    public class Card
    {
        [Column("card_id"), Key]
        public int CardId { get; set; }

        [Column("card_entity_id")]
        public int CardEntityId { get; set; }

        [Column("card_cardvariant_id")]
        public int CardCardVariantId { get; set; }

        [Column("card_rubles")]
        public int CardRubles { get; set; }

        [Column("card_enabled")]
        public bool CardEnabled { get; set; }

        public Entity Entity { get; set; }
        public CardVariant CardVariant { get; set; }
    }
}
