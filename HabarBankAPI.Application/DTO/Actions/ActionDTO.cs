using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.DTO.Actions
{
    public class ActionDTO
    {
        public long ActionId { get; set; }
        public long ActionTypeId { get; set; }
        public DateTime ActionDateTime { get; set; }
        public long SubstanceId { get; set; }
        public bool Enabled { get; set; }
    }
}
