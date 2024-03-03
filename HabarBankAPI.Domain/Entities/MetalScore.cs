
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities
{
    public class MetalScore : Substance
    {
        public MetalScore() { }

        public int MetalScoreId { get; private set; }
        public int MetalId { get; private set; }

        public int EntityId { get; set; }
    }
}
