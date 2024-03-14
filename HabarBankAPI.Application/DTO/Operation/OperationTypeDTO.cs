using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application
{
    public class OperationTypeDTO
    {
        public long OperationTypeId { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
