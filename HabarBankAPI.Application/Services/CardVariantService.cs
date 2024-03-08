using AutoMapper;
using HabarBankAPI.Application.DTO.Cards;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities.Card;
using HabarBankAPI.Domain.Exceptions.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.Services
{
    public class CardVariantService : ICardVariantService
    {
        private readonly IGenericRepository<CardVariant> _repository;
        private readonly Mapper _mapperA;
        private readonly Mapper _mapperB;

        public CardVariantService(IGenericRepository<CardVariant> repository, Mapper mapperA, Mapper mapperB)
        {
            this._repository = repository;
            this._mapperA = mapperA;
            this._mapperB = mapperB;
        }

        public async Task CreateNewCardVariant(CardVariantDTO cardVariantDTO)
        {
            CardVariant cardVariant = this._mapperA.Map<CardVariant>(cardVariantDTO);

            await Task.Run(() => this._repository.Create(cardVariant));
        }

        public async Task<IList<CardVariantDTO>> GetAllCardVariants()
        {
            IList<CardVariant> cardVariants = await Task.Run(() => this._repository.Get(x => x.Enabled is true).ToList());

            IList<CardVariantDTO> cardVariantDTOs = this._mapperB.Map<IList<CardVariantDTO>>(cardVariants);

            return cardVariantDTOs;
        }

        public async Task<CardVariantDTO> GetCardVariantById(long id)
        {
            CardVariant cardVariant = await Task.Run(() => this._repository.FindById(id));

            CardVariantDTO cardVariantDTO = this._mapperB.Map<CardVariantDTO>(cardVariant);

            return cardVariantDTO;
        }

        public async Task SetCardVariantEnabled(long id, bool enabled)
        {
            CardVariant cardVariant = await Task.Run(() => this._repository.FindById(id));

            if (cardVariant is null)
            {
                throw new CardVariantNotFoundException($"Не удалось найти вариант карты с идентификатором {id}");
            }

            cardVariant.SetEnabled(enabled);

            await Task.Run(() => this._repository.Update(cardVariant));
        }
    }
}
