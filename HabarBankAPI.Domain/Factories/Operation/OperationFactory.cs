using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Exceptions.Action;
using HabarBankAPI.Domain.Exceptions.Substance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Factories
{
    public class OperationFactory : IOperationFactory
    {
        private OperationType? _operationType = default;
        private Card? _card = default;
        private DateTime _dateTime = default;

        public Operation Build()
        {
            if (this._card is null)
            {
                throw new SubstanceNotFoundException("Объект для операции не может быть равен null");
            }

            if (this._dateTime == default)
            {
                throw new ArgumentNullException("Дата операции не может быть пустой");
            }

            Operation operation = new
            (
                this._operationType,
                this._dateTime,
                this._card
            );

            operation.SetEnabled(true);

            return operation;
        }

        public IOperationFactory WithActionType(OperationType? actionType)
        {
            this._operationType = actionType;
            return this;
        }

        public IOperationFactory WithDateTime(DateTime dateTime)
        {
            this._dateTime = dateTime;
            return this;
        }

        public IOperationFactory WithCard(Card? card)
        {
            this._card = card;
            return this;
        }
    }
}
