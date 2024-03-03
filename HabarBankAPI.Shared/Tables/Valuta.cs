using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabarBankAPI.Data.Entities
{
    [Table("valuta")]
    public class Valuta
    {
        [Column("valuta_id"), Key]
        public int ValutaId { get; set; }

        [Column("valuta_name")]
        public string ValutaName { get; set; }

        [Column("valuta_rubles")]
        public int ValutaRubles { get; set; }

        [Column("valuta_enabled")]
        public bool ValutaEnabled { get; set; }

        public ICollection<ValutaStorageVariant> ValutaStorageVariants { get; set;}
    }
}
