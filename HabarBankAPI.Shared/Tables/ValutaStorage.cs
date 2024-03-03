using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabarBankAPI.Data.Entities
{
    [Table("valutastorage")]
    public class ValutaStorage
    {
        [Column("valutastorage_id"), Key]
        public int ValutaStorageId { get; set; }

        [Column("valutastorage_entity_id")]
        public int ValutaStorageEntityId { get; set; }

        [Column("valutastorage_variant_id")]
        public int ValutaStorageVariantId { get; set; }

        [Column("valutastorage_rubles")]
        public int ValutaStorageRubles { get; set; }

        [Column("valutastorage_enabled")]
        public bool ValutaStorageEnabled { get; set; }

        public Entity Entity { get; set; }
        public ValutaStorageVariant ValutaStorageVariant { get; set; }
    }
}
