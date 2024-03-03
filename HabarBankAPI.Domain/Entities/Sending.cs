using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities
{
    public class Sending : Action
    {
        public Sending() { }

        public Sending(int sendingId, int substanceSenderId, int substanceRecipientId, bool sendingEnabled)
        {
            this.ActionId = sendingId;
            this.SubstanceSenderId = substanceSenderId;
            this.SubstanceRecipientId = substanceRecipientId;
            this.Enabled = sendingEnabled;
        }

        public int SubstanceSenderId { get; private init; }
        public int SubstanceRecipientId { get; private init; }
        public int RublesCount { get; private init; }

    }
}
