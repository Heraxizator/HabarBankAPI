using HabarBankAPI.Domain.Share;
using System.ComponentModel.DataAnnotations;

namespace HabarBankAPI.Domain.Entities.Operation
{
    public class Action : Entity, IAggregateRoot
    {
        public Action() { }

        public Action(long actionId, long actionTypeId, DateTime actionDateTime, long substanceId)
        {
            this.ActionId = actionId;
            this.ActionTypeId = actionTypeId;
            this.ActionDateTime = actionDateTime;
            this.SubstanceId = substanceId;
        }

        [Key]
        public long ActionId { get; protected set; }
        public long ActionTypeId { get; private init; }
        public DateTime ActionDateTime { get; protected set; }
        public long SubstanceId { get; protected set; }

        public void SetCurrentDateTime()
        {
            this.ActionDateTime = DateTime.Now;
        }
    }
}
