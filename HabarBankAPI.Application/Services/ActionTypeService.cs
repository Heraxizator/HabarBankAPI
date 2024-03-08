using AutoMapper;
using HabarBankAPI.Application.DTO.Actions;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities.Operation;
using HabarBankAPI.Domain.Exceptions.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.Services
{
    public class ActionTypeService : IActionTypeService
    {
        private readonly IGenericRepository<ActionType> _repository;
        private readonly Mapper _mapperA;
        private readonly Mapper _mapperB;

        public ActionTypeService(IGenericRepository<ActionType> repository, Mapper mapperA, Mapper mapperB)
        {
            this._repository = repository;
            this._mapperA = mapperA;
            this._mapperB = mapperB;
        }

        public async Task CreateNewActionType(ActionTypeDTO actionTypeDTO)
        {
            ActionType actionType = this._mapperA.Map<ActionType>(actionTypeDTO);

            await Task.Run(() => this._repository.Create(actionType));
        }

        public async Task<ActionTypeDTO> GetActionTypeById(long id)
        {
            ActionType? actionType = await Task.Run(
                () => this._repository.Get(x => x.ActionTypeId == id && x.Enabled is true).FirstOrDefault());

            ActionTypeDTO actionTypeDTO = this._mapperB.Map<ActionTypeDTO>(actionType);

            return actionTypeDTO;
        }

        public async Task<IList<ActionTypeDTO>> GetAllActionTypes()
        {
            IList<ActionType> actionTypes = await Task.Run(
                () => this._repository.Get(x => x.Enabled is true).ToList());

            IList<ActionTypeDTO> actionTypeDTOs = this._mapperB.Map<IList<ActionTypeDTO>>(actionTypes);

            return actionTypeDTOs;
        }

        public async Task SetActionTypeEnabled(long id, bool enabled)
        {
            ActionType? actionType = await Task.Run(
                () => this._repository.Get(x => x.ActionTypeId == id && x.Enabled is true).FirstOrDefault());

            if (actionType is null)
            {
                throw new ActionTypeNotFoundException($"Тип операции с идентификатором {id} не найден");
            }

            actionType.SetEnabled(enabled);

            await Task.Run(() => this._repository.Update(actionType));
        }
    }
}
