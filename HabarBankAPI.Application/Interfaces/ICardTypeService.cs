using HabarBankAPI.Application.DTO.Cards;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.Interfaces
{
    public interface ICardTypeService : IAppService
    {
        public Task<IList<CardTypeDTO>> GetAllCardTypes();
        public Task<CardTypeDTO> GetCardTypeById(long id);   
        public Task CreateNewCardType(CardTypeDTO cardTypeDTO);
        public Task SetCardTypeEnabled(long id, bool enabled);
    }
}
