
using AutoMapper;
using HabarBankAPI.Application.DTO.Actions;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Exceptions.Action;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = HabarBankAPI.Domain.Entities.Action;

namespace HabarBankAPI.Application.Services
{
    public class ActionService : IActionService
    {
        private readonly IGenericRepository<Action> _repository;

        private readonly Mapper _mapperA;
        private readonly Mapper _mapperB;

        public ActionService(
            IGenericRepository<Action> repository, Mapper mapperA, Mapper mapperB)
        {
            this._repository = repository;
            this._mapperA = mapperA;
            this._mapperB = mapperB;
        }

        public async Task CreateNewAction(ActionDTO actionDTO)
        {
            Action action = this._mapperA.Map<Action>(actionDTO);

            await Task.Run(() => this._repository.Create(action));
        }

        public async Task<ActionDTO> GetActionByActionId(int id)
        {
            Action action = await Task.Run(() => this._repository.FindById(id));

            if (action is null)
            {
                throw new ActionNotFoundException($"Операция с идентификатором {id} не найдена");
            }

            ActionDTO actionDTO = this._mapperB.Map<ActionDTO>(action);

            return actionDTO;
        }

        public async Task<IList<ActionDTO>> GetActionsByEntityId(int id)
        {
            IList<Action> actions = await Task.Run(
                () => this._repository.Get(x => x.SubstanceId == id && x.Enabled is true).ToList());

            IList<ActionDTO> actionDTO = this._mapperB.Map<IList<ActionDTO>>(actions);

            return actionDTO;
        }

        public async Task<IList<ActionDTO>> GetAllActions(int count)
        {
            IList<Action> actions = await Task.Run(
                () => this._repository.Get(x => x.Enabled is true).ToList());

            IList<ActionDTO> actionDTO = this._mapperB.Map<IList<ActionDTO>>(actions);

            return actionDTO;
        }

        public async Task SetActionEnabled(int id, bool enabled)
        {
            Action action = await Task.Run(() => this._repository.FindById(id));

            if (action is null)
            {
                throw new ActionNotFoundException($"Операция с идентификатором {id} не найдена");
            }

            action.SetEnabled(enabled);
        }
    }
}
