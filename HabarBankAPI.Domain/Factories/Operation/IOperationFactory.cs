using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain
{
    public interface IOperationFactory : IFactory<Operation>
    {
        IOperationFactory WithActionType(OperationType? actionType);
        IOperationFactory WithDateTime(DateTime dateTime);
        IOperationFactory WithCard(Card? card);
    }
}
