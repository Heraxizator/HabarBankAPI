using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabarBankAPI.Data.Entities
{
    [Table("operation")]
    public class Operation
    {
        [Column("operation_id"), Key]
        public int OperationId { get; set; }

        [Column("operation_operation_id")]
        public int OperationOperationId { get; set; }

        [Column("operation_datetime")]
        public DateTime OperationDatetime { get; set; }

        [Column("operation_entity_id")]
        public int OperationEntityId { get; set; }

        [Column("operation_enabled")]
        public bool OperationEnabled { get; set; }

        public OperationType OperationType { get; set; }
        public Transfer Transfer { get; set; }
        public Entity Entity { get; set; }
        public int TransferId { get; set; }
    }
}
