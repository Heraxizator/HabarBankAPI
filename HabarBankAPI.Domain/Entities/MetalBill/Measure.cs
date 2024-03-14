using HabarBankAPI.Domain.Share;
using System.ComponentModel.DataAnnotations;

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
        public long MeasureId { get; private set; }
        public string? Name { get; private set; }
        public int RublesCount { get; private set; }

    }
}
