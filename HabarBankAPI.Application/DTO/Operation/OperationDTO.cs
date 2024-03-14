using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application
{
    public class OperationDTO
    {
        public long OperationId { get; set; }
        public long OperationTypeId { get; set; }
        public DateTime OperationDateTime { get; set; }
        public long SubstanceId { get; set; }
        public bool Enabled { get; set; }
    }
}
