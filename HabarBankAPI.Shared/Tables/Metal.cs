using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabarBankAPI.Data.Entities
{
    [Table("metal")]
    public class Metal
    {
        [Column("matal_id"), Key]
        public int MetalId { get; set; }

        [Column("matal_name")]
        public string MetalName { get; set; }

        [Column("matal_measure_id")]
        public int MetalMeasureId { get; set; }

        [Column("metal_enabled")]
        public bool MetalEnabled { get; set; }

        public Measure Measure { get; set; }
        public ICollection<MetalStorage> MetalStorages { get; set; }
    }
}
