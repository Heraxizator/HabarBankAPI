using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.DTO.Transfers
{
    public class SendingDTO
    {
        public long ActionId { get; set; }
        public DateTime ActionDateTime { get; set; }
        public long SubstanceId { get; set; }
        public long SubstanceSenderId { get; set; }
        public long SubstanceRecipientId { get; set; }
        public int RublesCount { get; set; }
        public bool Enabled { get; set; }
    }
}
