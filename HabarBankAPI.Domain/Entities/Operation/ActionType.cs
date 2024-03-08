using HabarBankAPI.Domain.Share;
using System.ComponentModel.DataAnnotations;

namespace HabarBankAPI.Domain.Entities.Operation
{
    public class ActionType : Entity
    {
        public ActionType() { }

        public ActionType(string name, bool enabled)
        {
            this.Name = name;
            this.Enabled = enabled;
        }

        [Key]
        public long ActionTypeId { get; set; }
        public string Name { get; private set; }

        public void SetName(string name)
        {
            this.Name = name;
        }
    }
}
