using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Entities.Transfer;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Factories
{
    public interface ITransferFactory : IFactory<Sending>
    {
        ITransferFactory WithCardSender(Card? substanceSender);
        ITransferFactory WithCardRecipient(Card? substanceRecipient);
        ITransferFactory WithOperationType(OperationType? operationType);
        ITransferFactory WithRublesCount(int rublesCount);
    }
}
