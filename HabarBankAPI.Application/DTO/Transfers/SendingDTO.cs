using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.DTO.Transfers
{
    public class SendingDTO
    {
        public int ActionId { get; set; }
        public DateTime ActionDateTime { get; set; }
        public int SubstanceId { get; set; }
        public int SubstanceSenderId { get; set; }
        public int SubstanceRecipientId { get; set; }
        public int RublesCount { get; set; }
        public bool Enabled { get; set; }
    }
}
