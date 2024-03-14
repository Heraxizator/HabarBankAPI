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
        public Task<IList<OperationTypeDTO>> GetAllActionTypes();
        public Task<OperationTypeDTO> GetActionTypeById(long id);
        public Task CreateNewActionType(OperationTypeDTO actionTypeDTO);
        public Task SetActionTypeEnabled(long id, bool enabled);
    }
}
