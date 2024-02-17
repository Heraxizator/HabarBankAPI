using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabarBankAPI.Data.Entities
{
    [Table("transfer")]
    public class Transfer
    {
        [Column("transfer_id"), Key]
        public int TransferId { get; set; }

        [Column("transfer_entitysender_id")]
        public int TransferEntitySenderId { get; set; }

        [Column("transfer_entityrecipient_id")]
        public int TransferEntityRecipientId { get; set; }

        [Column("transfer_operation_id")]
        public int TransferOperationId { get; set; }

        [Column("transfer_enabled")]
        public bool TransferEnabled { get; set; }

        public Operation Operation { get; set; }
        public int OperationId { get; set; }
    }
}
