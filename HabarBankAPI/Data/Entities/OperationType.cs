using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabarBankAPI.Data.Entities
{
    [Table("operationtype")]
    public class OperationType
    {
        [Column("operationtype_id"), Key]
        public int OperationTypeId { get; set; }

        [Column("operationtype_name")]
        public string OperationTypeName { get; set; }

        [Column("operationtype_enabled")]
        public bool OperationTypeEnabled { get; set; }

        public ICollection<Operation> Operations { get; set; }
    }
}
