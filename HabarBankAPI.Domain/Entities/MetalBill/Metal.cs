using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities.MetalScore.MetalScore
{
    public class Metal : Entity
    {
        public Metal() { }

        [Key]
        public long MetalId { get; private set; }
        public string Name { get; private set; }
        public int RublesCount { get; private set; }
        public long MeasureId { get; private set; }

    }
}
