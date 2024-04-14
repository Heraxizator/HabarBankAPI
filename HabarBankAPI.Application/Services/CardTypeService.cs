using AutoMapper;
using HabarBankAPI.Application.DTO.Cards;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Exceptions.Card;
using HabarBankAPI.Infrastructure.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.Services
{
    public class CardTypeService : ICardTypeService
    {
        private readonly IGenericRepository<CardType> _repository;
        private readonly AppUnitOfWork _unitOfWork;

        private readonly Mapper _mapperA;
        private readonly Mapper _mapperB;

        public CardTypeService(
            IGenericRepository<CardType> repository,
            AppUnitOfWork unitOfWork,
            Mapper mapperA,
            Mapper mapperB) 
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            this._mapperA = mapperA;
            this._mapperB = mapperB;
        }

        public async Task CreateNewCardType(CardTypeDTO cardTypeDTO)
        {
            CardType cardType = this._mapperA.Map<CardType>(cardTypeDTO);

            await Task.Run(() => this._repository.Create(cardType));

            await this._unitOfWork.Commit();
        }

        public async Task<IList<CardTypeDTO>> GetAllCardTypes()
        {
            IList<CardType> cardTypes = await Task.Run(() => this._repository.Get(x => x.Enabled is true).ToList());

            IList<CardTypeDTO> cardTypeDTOs = this._mapperB.Map<IList<CardTypeDTO>>(cardTypes);

            return cardTypeDTOs;
        }

        public async Task<CardTypeDTO> GetCardTypeById(long id)
        {
            CardType? cardType = await Task.Run(
                () => this._repository.Get(x => x.CardTypeId == id && x.Enabled is true).FirstOrDefault());

            CardTypeDTO cardTypeDTO = this._mapperB.Map<CardTypeDTO>(cardType);

            return cardTypeDTO;
        }

        public async Task SetCardTypeEnabled(long id, bool enabled)
        {
            CardType? cardType = await Task.Run(
                () => this._repository.Get(x => x.CardTypeId == id && x.Enabled is true).FirstOrDefault());

            if (cardType is null)
            {
                throw new CardTypeNotFoundException($"Не удалось найти тип карты с идентификатором {id}");
            }

            cardType.SetEnabled(enabled);

            await Task.Run(() => this._repository.Update(cardType));

            await this._unitOfWork.Commit();
        }
    }
}
