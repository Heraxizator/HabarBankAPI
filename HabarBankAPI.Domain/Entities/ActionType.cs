
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities
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
        public int ActionTypeId { get; set; }
        public string Name { get; private set; }   

        public void SetName(string name)
        {
            this.Name = name;
        }
    }
}
