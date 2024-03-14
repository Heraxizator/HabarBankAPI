using AutoMapper;
using HabarBankAPI.Application.DTO.Transfers;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Domain;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Entities.Transfer;
using HabarBankAPI.Domain.Entities.ValutaBill;
using HabarBankAPI.Domain.Exceptions.Action;
using HabarBankAPI.Domain.Exceptions.Sending;
using HabarBankAPI.Domain.Exceptions.Substance;
using HabarBankAPI.Domain.Exceptions.User;
using HabarBankAPI.Domain.Factories;
using HabarBankAPI.Domain.Factories.Transfer;
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Uow;

namespace HabarBankAPI.Application.Services
{
    public class SendingService : ISendingService
    {
        private readonly Mapper _mapperA;
        private readonly Mapper _mapperB;

        private readonly GenericRepository<Sending> _sendings_repository;
        private readonly GenericRepository<Card> _cards_repository;
        private readonly GenericRepository<User> _users_repository;
        private readonly GenericRepository<OperationType> _operationtypes_repository;
        private readonly UnitOfWork _unitOfWork;

        public SendingService(
            Mapper mapperA,
            Mapper mapperB,
            GenericRepository<Sending> sendingsRepository,
            GenericRepository<Card> cardsRepository,
            GenericRepository<User> usersRepository,
            GenericRepository<OperationType> operationTypesRepository,
            UnitOfWork unitOfWork)
        {
            this._mapperA = mapperA;
            this._mapperB = mapperB;
            this._sendings_repository = sendingsRepository;
            this._cards_repository = cardsRepository;
            this._users_repository = usersRepository;
            this._operationtypes_repository = operationTypesRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task CreateTransfer(SendingDTO sendingDTO)
        {
            Card? senderCard = this._cards_repository
                .GetWithInclude(card => card.Transfers)
                .FirstOrDefault(card => card.CardId == sendingDTO.SubstanceSenderId && card.Enabled is true);

            if (senderCard is null)
            {
                throw new SubstanceNotFoundException("Идентификатор отправителя не является правильным");
            }

            Card? recipientCard = this._cards_repository
                .GetWithInclude(card => card.Transfers)
                .FirstOrDefault(card => card.CardId == sendingDTO.SubstanceRecipientId && card.Enabled is true);

            if (recipientCard is null)
            {
                throw new SubstanceNotFoundException("Идентификатор отправителя не является правильным");
            }

            int sendingRubles = sendingDTO.RublesCount;

            if (sendingRubles <= 0)
            {
                throw new SubstanceArgumentException("Сумма перевода должна быть больше нуля");
            }

            senderCard.RublesCount -= sendingRubles;

            recipientCard.RublesCount += sendingRubles;

            OperationType? operationType = this._operationtypes_repository.Get(
                operationType => operationType.OperationTypeId == sendingDTO.OperationTypeId && operationType.Enabled is true).FirstOrDefault();

            if (operationType is null)
            {
                throw new OperationTypeNotFoundException($"Тип операции с идентификатором {sendingDTO.OperationTypeId} не найден");
            }
            
            TransferFactory transferFactory = new();

            Sending sending = transferFactory
                .WithCardSender(senderCard)
                .WithCardRecipient(recipientCard)
                .WithOperationType(operationType)
                .WithRublesCount(sendingDTO.RublesCount)
            .Build();

            senderCard.Transfers.Clear();

            senderCard.Transfers.Add(sending);

            await Task.Run(() => this._cards_repository.Update(senderCard));

            await this._unitOfWork.Commit();
        }

        public async Task<SendingDTO> GetTransferByTransferId(long sendingId)
        {
            Sending? sending = await Task.Run(
                () => this._sendings_repository
                .GetWithInclude(sending => sending.Card, sending => sending.CardRecipient, sending => sending.CardSender, sending => sending.OperationType)
                .FirstOrDefault(sending => sending.SendingId == sendingId && sending.Enabled is true));

            SendingDTO sendingDTO = PrepareTransferDTO(sending);

            return sendingDTO;
        }

        public async Task<IList<SendingDTO>> GetTransfersBySubstanceId(long substanceId)
        {
            IList<Sending> sendings = await Task.Run(
                () => this._sendings_repository
                .GetWithInclude(sending => sending.Card, sending => sending.CardSender, sending => sending.CardRecipient, sending => sending.OperationType)
                .Where(sending => sending.Card.CardId == substanceId && sending.Enabled is true)
                .ToList());

            IList<SendingDTO> sendingDTOs = PrepareTransferDTOs(sendings);

            return sendingDTOs;
        }

        public async Task<IList<SendingDTO>> GetTransfersByUserId(long userId)
        {
            User? user = await Task.Run(() => this._users_repository.Get(
                user => user.UserId == userId && user.Enabled is true).FirstOrDefault());

            if (user is null)
            {
                throw new UserNotFoundException($"Пользователь с идентификатором {userId} не найден");
            }

            IList<Sending> sendings = await Task.Run(
                () => this._sendings_repository
                .GetWithInclude(sending => sending.Card, sending => sending.CardRecipient, sending => sending.CardSender, sending => sending.OperationType)
                .Where(sending => sending.Card?.User?.UserId == userId && sending.Enabled is true).ToList());

            IList<SendingDTO> sendingDTOs = PrepareTransferDTOs(sendings);

            return sendingDTOs;
        }

        public async Task SetTransferStatus(long sendingId, bool sendingEnabled)
        {
            Sending? sending = this._sendings_repository.Get(sending => sending.SendingId == sendingId).FirstOrDefault();

            if (sending is null)
            {
                throw new SendingNotFoundException($"Перевод с идентификатором {sendingId} не найден");
            }

            sending.SetEnabled(sendingEnabled);

            await Task.Run(() => this._sendings_repository.Update(sending));

            await this._unitOfWork.Commit();
        }

        internal IList<SendingDTO> PrepareTransferDTOs(IList<Sending> sendings)
        {
            IList<SendingDTO> sendingDTOs = new List<SendingDTO>();

            foreach (Sending sending in sendings)
            {
                SendingDTO sendingDTO = PrepareTransferDTO(sending);

                sendingDTOs.Add(sendingDTO);
            }

            return sendingDTOs;
        }

        internal SendingDTO PrepareTransferDTO(Sending? sending)
        {
            SendingDTO sendingDTO = this._mapperB.Map<SendingDTO>(sending);

            sendingDTO.SubstanceId = sending?.Card is null ? 0 : sending.Card.CardId;

            sendingDTO.SubstanceSenderId = sending?.CardSender is null ? 0 : sending.CardSender.CardId;

            sendingDTO.SubstanceRecipientId = sending?.CardRecipient is null ? 0 : sending.CardRecipient.CardId;

            sendingDTO.OperationTypeId = sending?.OperationType is null ? 0 : sending.OperationType.OperationTypeId;

            return sendingDTO;
        }
    }
}
