using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabarBankAPI.Data.Entities
{
    [Table("valutastoragevariant")]
    public class ValutaStorageVariant
    {
        [Column("valutastoragevariant_id"), Key]
        public int ValutaStorageVariantId { get; set; }

        [Column("valutastoragevariant_name")]
        public string ValutaStorageVariantName { get; set; }

        [Column("valutastoragevariant_text")]
        public string ValutaStorageVariantText { get; set; }

        [Column("valutastoragevariant_valuta_id")]
        public int ValutaStorageVariantValutaId { get; set; }

        [Column("valutastoragevariant_percentage")]
        public int ValutaStorageVariantPercentage { get; set; }

        [Column("valutastoragevariant_userlevel_id")]
        public int ValutaStorageVariantUserlevelId { get; set; }

        [Column("valutastoragevariant_enabled")]
        public bool ValutaStorageVariantEnabled { get; set; }

        public UserLevel UserLevel { get; set; }
        public Valuta Valuta { get; set; }
        public ICollection<ValutaStorage> valutaStorages { get; set; }
    }
}
