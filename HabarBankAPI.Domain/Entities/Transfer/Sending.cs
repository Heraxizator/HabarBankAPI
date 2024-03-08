using HabarBankAPI.Domain.Entities.Operation;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = HabarBankAPI.Domain.Entities.Operation.Action;

namespace HabarBankAPI.Domain.Entities.Transfer
{
    public class Sending : Action, IAggregateRoot
    {
        public Sending() { }

        public Sending(long sendingId, int substanceSenderId, int substanceRecipientId, bool sendingEnabled)
        {
            this.ActionId = sendingId;
            this.SubstanceSenderId = substanceSenderId;
            this.SubstanceRecipientId = substanceRecipientId;
            this.Enabled = sendingEnabled;
        }

        public long SubstanceSenderId { get; private init; }
        public long SubstanceRecipientId { get; private init; }
        public int RublesCount { get; private init; }

    }
}
