﻿using HabarBankAPI.Application.DTO.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.Interfaces
{
    public interface ICardVariantService
    {
        public Task CreateNewCardVariant(CardVariantDTO cardVariantDTO);
        public Task<IList<CardVariantDTO>> GetAllCardVariants();
        public Task<CardVariantDTO> GetCardVariantById(int id);
        public Task SetCardVariantEnabled(int id, bool enabled);
    }
}
