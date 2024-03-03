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
        public Task CreateCard(int userId, CardDTO cardDTO);
        public Task<IList<CardDTO>> GetCardsByUserId(int userId);
        public Task EditCardEnabled(int id, bool enabled);
        public Task<CardDTO> GetCardData(int id);
        public Task AddPercentage(int id);
    }
}
