
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.Interfaces
{
    public interface IOperationService : IAppService
    {
        public Task<IList<OperationDTO>> GetAllActions(int count);
        public Task<IList<OperationDTO>> GetActionsByEntityId(long id);
        public Task<OperationDTO> GetActionByActionId(long id);
        public Task CreateNewAction(OperationDTO actionDTO);
        public Task SetActionEnabled(long id, bool enabled);
    }
}
