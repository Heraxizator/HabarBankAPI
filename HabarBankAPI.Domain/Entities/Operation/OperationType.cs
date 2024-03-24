using HabarBankAPI.Domain.Share;
using System.ComponentModel.DataAnnotations;

namespace HabarBankAPI.Domain.Entities
{
    public class OperationType : Entity
    {
        public OperationType() { }

        public OperationType(string name, bool enabled)
        {
            this.Name = name;
            this.Enabled = enabled;
        }

        [Key]
        public long OperationTypeId { get; private set; }
        public string? Name { get; private set; }

        public void SetName(string name)
        {
            this.Name = name;
        }
    }
}
