using HabarBankAPI.Domain.Entities.ValutaBill;
using HabarBankAPI.Domain.Share;

namespace HabarBankAPI.Domain.Entities
{
    public abstract class Action : Entity, IAggregateRoot
    {
        public string OperationDateTime { get; protected set; }
        public OperationType OperationType { get; protected set; }
        public Card Card { get; protected set; }

        public void SetCurrentDateTime()
        {
            this.OperationDateTime = DateTime.Now.ToString();
        }
    }
}
