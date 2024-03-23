using HabarBankAPI.Domain.Exceptions.Sending;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Entities.Transfer
{
    public class Sending : Action, IAggregateRoot
    {
        public Sending() { }

        public Sending(Card? cardSender, Card? cardRecipient, OperationType? operationType, int rublesCount, bool sendingEnabled)
        {
            this.CardSender = cardSender;
            this.CardRecipient = cardRecipient;
            this.OperationType = operationType;
            this.RublesCount = rublesCount;
            this.Enabled = sendingEnabled;
        }

        [Key]
        public long SendingId { get; private init; }
        public Card CardSender { get; private init; }
        public Card CardRecipient { get; private init; }
        public int RublesCount { get; private init; }

        public void RunSending()
        {
            if (this.CardSender.RublesCount < this.RublesCount)
            {
                throw new InvalidSendingException("Недостаточно средств для осуществления перевода");
            }

            this.CardSender.RublesCount -= this.RublesCount;
            this.CardRecipient.RublesCount += this.RublesCount;
        }
    }
}
