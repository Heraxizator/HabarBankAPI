using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities.MetalBill
{
    public class MetalScore : Substance.Substance, IAggregateRoot
    {
        public MetalScore() { }

        public long MetalScoreId { get; private set; }
        public long MetalId { get; private set; }

        public long EntityId { get; set; }
    }
}
