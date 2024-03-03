
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities
{
    public class Measure : Entity
    {
        public Measure() { }

        public Measure(string name, int rublesCount, bool enabled)
        {
            this.Name = name;
            this.RublesCount = rublesCount;
            this.Enabled = enabled;
        }

        [Key]
        public int MeasureId { get; private set; }
        public string Name { get; private set; }
        public int RublesCount { get; private set; }

    }
}
