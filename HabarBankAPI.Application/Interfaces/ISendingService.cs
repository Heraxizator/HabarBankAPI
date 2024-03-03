using HabarBankAPI.Application.DTO.Transfers;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.Interfaces
{
    public interface ISendingService : IAppService
    {
        public Task CreateTransfer(SendingDTO sendingDTO);
        public Task<SendingDTO> GetTransferByTransferId(int sendingId);
        public Task<IList<SendingDTO>> GetTransfersBySubstanceId(int substanceId);
        public Task<IList<SendingDTO>> GetTransfersByUserId(int userId);
        public Task SetTransferStatus(int sendingId, bool sendingEnabled);
    }
}
