
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities
{
    public class Action : Entity
    {
        public Action() { }

        public Action(int actionId, int actionTypeId, DateTime actionDateTime, int substanceId)
        {
            this.ActionId = actionId;
            this.ActionTypeId = actionTypeId;
            this.ActionDateTime = actionDateTime;
            this.SubstanceId = substanceId;
        }

        [Key]
        public int ActionId { get; protected set; }
        public int ActionTypeId { get; private init; }
        public DateTime ActionDateTime { get; protected set; }
        public int SubstanceId { get; protected set; }

        public void SetCurrentDateTime()
        {
            this.ActionDateTime = DateTime.Now;
        }
    }
}
