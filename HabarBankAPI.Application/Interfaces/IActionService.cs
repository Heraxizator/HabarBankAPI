using HabarBankAPI.Application.DTO.Actions;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.Interfaces
{
    public interface IActionService : IAppService
    {
        public Task<IList<ActionDTO>> GetAllActions(int count);
        public Task<IList<ActionDTO>> GetActionsByEntityId(long id);
        public Task<ActionDTO> GetActionByActionId(long id);
        public Task CreateNewAction(ActionDTO actionDTO);
        public Task SetActionEnabled(long id, bool enabled);
    }
}
