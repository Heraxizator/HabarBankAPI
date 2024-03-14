
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities
{
    public class MetalScore : Substance, IAggregateRoot
    {
        public MetalScore() { }

        [Key]
        public long MetalScoreId { get; private set; }
        public Metal? Metal { get; private set; }
    }
}
