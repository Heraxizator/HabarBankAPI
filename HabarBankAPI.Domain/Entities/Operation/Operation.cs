
using HabarBankAPI.Domain.Entities.ValutaBill;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities
{
    public class Operation : Action, IAggregateRoot
    {
        public Operation()
        {
        }

        public Operation(OperationType? operationType, DateTime operationDateTime, Card? card)
        {
            this.OperationType = operationType; 
            this.OperationDateTime = operationDateTime;
            this.Card = card;
        }

        [Key]
        public long OperationId { get; private init; }
    }
}
