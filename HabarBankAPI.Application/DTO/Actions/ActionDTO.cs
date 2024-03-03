using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.DTO.Actions
{
    public class ActionDTO
    {
        public int ActionId { get; set; }
        public int ActionTypeId { get; set; }
        public DateTime ActionDateTime { get; set; }
        public int SubstanceId { get; set; }
        public bool Enabled { get; set; }
    }
}
