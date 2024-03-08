using HabarBankAPI.Application.DTO.Cards;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.Interfaces
{
    public interface ICardService : IAppService
    {
        public Task CreateCard(long userId, CardDTO cardDTO);
        public Task<IList<CardDTO>> GetCardsByUserId(long userId);
        public Task EditCardEnabled(long id, bool enabled);
        public Task<CardDTO> GetCardData(long id);
        public Task AddPercentage(long id);
    }
}
