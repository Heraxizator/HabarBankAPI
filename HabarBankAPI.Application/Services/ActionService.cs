
using AutoMapper;
using HabarBankAPI.Application.DTO.Actions;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Domain;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Exceptions.Action;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = HabarBankAPI.Domain.Entities.Operation.Action;

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

        public async Task<ActionDTO> GetActionByActionId(long id)
        {
            ActionByIdSpecification specification = new();

            Action? action = await Task.Run(() => this._repository.Get(x => specification.IsSatisfiedBy((x, id))).FirstOrDefault());

            ActionDTO actionDTO = this._mapperB.Map<ActionDTO>(action);

            return actionDTO;
        }

        public async Task<IList<ActionDTO>> GetActionsByEntityId(long id)
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

        public async Task SetActionEnabled(long id, bool enabled)
        {
            ActionByIdSpecification specification = new();

            Action? action = await Task.Run(() => this._repository.Get(x => specification.IsSatisfiedBy((x, id))).FirstOrDefault());

            if (action is null)
            {
                throw new ActionNotFoundException($"Операция с идентификатором {id} не найдена");
            }

            action.SetEnabled(enabled);
        }
    }
}
