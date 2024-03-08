using HabarBankAPI.Application.DTO.Actions;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.Interfaces
{
    public interface IActionTypeService : IAppService
    {
        public Task<IList<ActionTypeDTO>> GetAllActionTypes();
        public Task<ActionTypeDTO> GetActionTypeById(long id);
        public Task CreateNewActionType(ActionTypeDTO actionTypeDTO);
        public Task SetActionTypeEnabled(long id, bool enabled);
    }
}
