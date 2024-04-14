using AutoMapper;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Domain;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Entities.ValutaBill;
using HabarBankAPI.Domain.Exceptions.Action;
using HabarBankAPI.Domain.Exceptions.Substance;
using HabarBankAPI.Domain.Factories;
using HabarBankAPI.Domain.Share;
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Uow;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.Services
{
    public class OperationService : IOperationService
    {
        private readonly GenericRepository<Operation> _operations_repository;
        private readonly GenericRepository<OperationType> _operationtypes_repository;
        private readonly GenericRepository<Card> _cards_repository;
        private readonly AppUnitOfWork _unitOfWork;

        private readonly Mapper _mapperA;
        private readonly Mapper _mapperB;

        public OperationService(
            GenericRepository<Operation> operationsRepository,
            GenericRepository<OperationType> operationTypesRepository,
            GenericRepository<Card> cardsRepository,
            AppUnitOfWork unitOfWork,
            Mapper mapperA, Mapper mapperB)
        {
            this._operations_repository = operationsRepository;
            this._operationtypes_repository = operationTypesRepository;
            this._cards_repository = cardsRepository;
            this._unitOfWork = unitOfWork;
            this._mapperA = mapperA;
            this._mapperB = mapperB;
        }

        public async Task CreateNewAction(OperationDTO actionDTO)
        {
            OperationType? operationType = await Task.Run(
                () => this._operationtypes_repository.Get(
                    operationType => operationType.OperationTypeId == actionDTO.OperationTypeId && operationType.Enabled is true).FirstOrDefault());

            if (operationType is null)
            {
                throw new OperationTypeNotFoundException($"Тип операции с идентификатором {actionDTO.OperationTypeId} не найден");
            }

            Card? card = await Task.Run(
                () => this._cards_repository
                .GetWithInclude(card => card.Operations, card => card.CardVariant, card => card.CardVariant, card => card.User, card => card.Transfers)
                .FirstOrDefault(card => card.CardId == actionDTO.SubstanceId));

            if (card is null)
            {
                throw new SubstanceNotFoundException($"Счёт с идентификатором {actionDTO.SubstanceId} не найден");
            }

            OperationFactory operationFactory = new();

            Operation operation = operationFactory
                .WithActionType(operationType)
                .WithDateTime(DateTime.Now)
                .WithCard(card)
                .Build();

            card.Operations.Clear();

            card.Operations.Add(operation);

            await Task.Run(() => this._cards_repository.Update(card));

            await this._unitOfWork.Commit();
        }

        public async Task<OperationDTO> GetActionByActionId(long id)
        {
            OperationByIdSpecification specification = new();

            Operation? action = await Task.Run(() => this._operations_repository
            .GetWithInclude(action => action.Card, action => action.OperationType)
            .FirstOrDefault(action => specification.IsSatisfiedBy((action, id))));

            OperationDTO actionDTO = PrepareOperationDTO(action);

            return actionDTO;
        }

        public async Task<IList<OperationDTO>> GetActionsByEntityId(long id)
        {
            IList<Operation> operations = await Task.Run(
                () => this._operations_repository
                .GetWithInclude(action => action.Card, action => action.OperationType)
                .Where(action => action.Card?.CardId == id && action.Enabled is true).ToList());

            IList<OperationDTO> actionDTO = PrepareOperationDTOs(operations);

            return actionDTO;
        }

        public async Task<IList<OperationDTO>> GetAllActions(int count)
        {
            IList<Operation> actions = await Task.Run(
                () => this._operations_repository
                .GetWithInclude(action => action.Card, action => action.OperationType)
                .Where(action => action.Enabled is true).ToList());

            IList<OperationDTO> actionDTO = PrepareOperationDTOs(actions);

            return actionDTO;
        }

        public async Task SetActionEnabled(long id, bool enabled)
        {
            Operation? action = await Task.Run(() => this._operations_repository.Get(action => action.OperationId == id).FirstOrDefault());

            if (action is null)
            {
                throw new OperationNotFoundException($"Операция с идентификатором {id} не найдена");
            }

            action.SetEnabled(enabled);

            await Task.Run(() => this._operations_repository.Update(action));

            await this._unitOfWork.Commit();
        }

        internal IList<OperationDTO> PrepareOperationDTOs(IList<Operation> operations)
        {
            IList<OperationDTO> operationDTOs = new List<OperationDTO>();

            foreach (Operation operation in operations)
            {
                OperationDTO operationDTO = PrepareOperationDTO(operation);

                operationDTOs.Add(operationDTO);
            }

            return operationDTOs;
        }

        internal OperationDTO PrepareOperationDTO(Operation? operation)
        {
            OperationDTO actionDTO = this._mapperB.Map<OperationDTO>(operation);

            actionDTO.SubstanceId = operation is null ? 0 : operation.Card.CardId;

            actionDTO.OperationTypeId = operation is null ? 0 : operation.OperationType.OperationTypeId;

            return actionDTO;
        }
    }
}
