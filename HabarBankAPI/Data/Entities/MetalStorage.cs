using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabarBankAPI.Data.Entities
{
    [Table("metalstorage")]
    public class MetalStorage
    {
        [Column("metalstorage_id"), Key]
        public int MetalStotageId { get; set; }

        [Column("metalstorage_entity_id")]
        public int MetalStorageEntityId { get; set; }

        [Column("metalstorage_metal_id")]
        public int metalStorageMetalId { get; set; }

        [Column("metalstorage_rubles")]
        public int MetalStorageRubles { get; set; }

        [Column("metalstorage_enabled")]
        public bool MetalStorageEnabled { get; set; }

        public Entity Entity { get; set; }
        public Metal Metal { get; set; }
    }
}
