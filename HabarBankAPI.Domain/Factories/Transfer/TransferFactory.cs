using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Entities.Transfer;
using HabarBankAPI.Domain.Exceptions.Action;
using HabarBankAPI.Domain.Exceptions.Substance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Factories.Transfer
{
    public class TransferFactory : ITransferFactory
    {
        private Card? _cardRecipient = default;
        private Card? _cardSender = default;
        private OperationType? _operationType = default;
        private int _rublesCount;
        private DateTime _operationDateTime;

        public Sending Build()
        {
            if (this._cardRecipient is null)
            {
                throw new SubstanceNotFoundException("Счёт получателя не найден");
            }

            if (this._cardSender is null)
            {
                throw new SubstanceNotFoundException("Счёт отправителя не найден");
            }

            if (this._operationType is null)
            {
                throw new OperationTypeNotFoundException("Тип операции не найден");
            }

            if (this._rublesCount <= 0)
            {
                throw new ArgumentException("Рублей при переводе должно быть больше нуля");
            }

            if (this._operationDateTime.Year < DateTime.Now.Year)
            {
                throw new ArgumentException("Некорректная дата для перевода");
            }

            return new Sending
            (
                this._cardSender,
                this._cardRecipient,
                this._operationType,
                this._rublesCount,
                this._operationDateTime,
                true
            );
        }

        public ITransferFactory WithRublesCount(int rublesCount)
        {
            this._rublesCount = rublesCount;
            return this;
        }

        public ITransferFactory WithCardRecipient(Card? cardRecipient)
        {
            this._cardRecipient = cardRecipient;
            return this;
        }

        public ITransferFactory WithCardSender(Card? cardSender)
        {
            this._cardSender = cardSender;
            return this;
        }

        public ITransferFactory WithOperationType(OperationType? operationType)
        {
            this._operationType = operationType;
            return this;
        }

        public ITransferFactory WithOperationTime(DateTime operationTime)
        {
            this._operationDateTime = operationTime;
            return this;
        }
    }
}
