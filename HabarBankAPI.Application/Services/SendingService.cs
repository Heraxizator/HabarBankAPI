using AutoMapper;
using HabarBankAPI.Application.DTO.Transfers;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Domain;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities.Substance;
using HabarBankAPI.Domain.Entities.Transfer;
using HabarBankAPI.Domain.Exceptions.Sending;
using HabarBankAPI.Domain.Exceptions.Substance;

namespace HabarBankAPI.Application.Services
{
    public class SendingService : ISendingService
    {
        private readonly Mapper _mapperA;
        private readonly Mapper _mapperB;
        private readonly IGenericRepository<Sending> _sendings_repository;
        private readonly IGenericRepository<Substance> _substances_repository;

        public SendingService(Mapper mapperA, Mapper mapperB, 
            IGenericRepository<Sending> sendingsRepository, IGenericRepository<Substance> substanceRepository)
        {
            this._mapperA = mapperA;
            this._mapperB = mapperB;
            this._sendings_repository = sendingsRepository;
            this._substances_repository = substanceRepository;
        }

        public async Task CreateTransfer(SendingDTO sendingDTO)
        {
            Sending sending = this._mapperA.Map<Sending>(sendingDTO);

            long senderId = sendingDTO.SubstanceSenderId;

            long recipientId = sendingDTO.SubstanceRecipientId;

            SubstanceByIdSpecification substanceByIdSpecification = new();

            Substance? senderSubstance = this._substances_repository.Get(
                x => substanceByIdSpecification.IsSatisfiedBy((x, senderId))).FirstOrDefault();

            if (senderSubstance is null)
            {
                throw new SubstanceNotFoundException("Идентификатор отправителя не является правильным");
            }

            Substance? recipientSubstance = this._substances_repository.Get(
                x => substanceByIdSpecification.IsSatisfiedBy((x, recipientId))).FirstOrDefault();

            if (recipientSubstance is null)
            {
                throw new SubstanceNotFoundException("Идентификатор отправителя не является правильным");
            }

            int sendingRubles = sendingDTO.RublesCount;

            if (sendingRubles <= 0)
            {
                throw new SubstanceArgumentException("Сумма перевода должна быть больше нуля");
            }

            senderSubstance.RublesCount -= sendingRubles;

            recipientSubstance.RublesCount += sendingRubles;

            await Task.Run(() => this._substances_repository.Update(senderSubstance));

            await Task.Run(() => this._substances_repository.Update(recipientSubstance));

            await Task.Run(() => this._sendings_repository.Create(sending));
        }

        public async Task<SendingDTO> GetTransferByTransferId(long sendingId)
        {
            SendingByIdSpecification specification = new();

            Sending? sending = await Task.Run(
                () => this._sendings_repository.Get(x => specification.IsSatisfiedBy((x, sendingId))).FirstOrDefault());

            SendingDTO sendingDTO = this._mapperB.Map<SendingDTO>(sending); 

            return sendingDTO;
        }

        public async Task<IList<SendingDTO>> GetTransfersBySubstanceId(long substanceId)
        {
            SubstanceByIdSpecification specification = new();

            IList<Substance> substances = await Task.Run(
                () => this._substances_repository.Get(x => specification.IsSatisfiedBy((x, substanceId))).ToList());

            IList<long> substancesIds = substances.Select(x => x.SubstanceId).ToList();

            IList<Sending> sendings = await Task.Run(
                () => this._sendings_repository.Get(x => substancesIds.Contains(x.SubstanceId)).ToList());

            IList<SendingDTO> sendingDTOs = this._mapperB.Map<IList<SendingDTO>>(sendings);

            return sendingDTOs;
        }

        public async Task<IList<SendingDTO>> GetTransfersByUserId(long userId)
        {
            SubstanceByIdSpecification specification = new();

            IList<Substance> substances = await Task.Run(
                () => this._substances_repository.Get(x => x.AccountId == userId && x.Enabled is true).ToList());

            IList<long> substancesIds = substances.Select(x => x.SubstanceId).ToList();

            IList<Sending> sendings = await Task.Run(
                () => this._sendings_repository.Get(x => substancesIds.Contains(x.SubstanceId) && x.Enabled is true).ToList());

            IList<SendingDTO> sendingDTOs = this._mapperB.Map<IList<SendingDTO>>(sendings);

            return sendingDTOs;
        }

        public async Task SetTransferStatus(long sendingId, bool sendingEnabled)
        {
            SendingByIdSpecification specification = new();

            Sending? sending = this._sendings_repository.Get(x => specification.IsSatisfiedBy((x, sendingId))).FirstOrDefault();

            if (sending is null)
            {
                throw new SendingNotFoundException($"Перевод с идентификатором {sendingId} не найден");
            }

            sending.SetEnabled(sendingEnabled);

            await Task.Run(() => this._sendings_repository.Update(sending));
        }
    }
}
