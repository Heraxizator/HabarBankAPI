using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabarBankAPI.Data.Entities
{
    [Table("measure")]
    public class Measure
    {
        [Column("measure_id"), Key]
        public int MeasureId { get; set; }

        [Column("measure_name")]
        public string MeasureName { get; set; }

        [Column("measure_rubles")]
        public int MeasureRubles { get; set; }

        [Column("measure_enabled")]
        public bool MeasureEnabled { get; set; }

        public ICollection<Metal> Metals { get; set; }
    }
}
