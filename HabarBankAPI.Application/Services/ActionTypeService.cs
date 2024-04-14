using AutoMapper;
using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Exceptions.Action;
using HabarBankAPI.Infrastructure.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.Services
{
    public class ActionTypeService : IActionTypeService
    {
        private readonly IGenericRepository<OperationType> _repository;
        private readonly AppUnitOfWork _unitOfWork;
        private readonly Mapper _mapperA;
        private readonly Mapper _mapperB;

        public ActionTypeService(
            IGenericRepository<OperationType> repository,
            AppUnitOfWork unitOfWork,
            Mapper mapperA,
            Mapper mapperB)
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
            this._mapperA = mapperA;
            this._mapperB = mapperB;
        }

        public async Task CreateNewActionType(OperationTypeDTO actionTypeDTO)
        {
            OperationType actionType = this._mapperA.Map<OperationType>(actionTypeDTO);

            await Task.Run(() => this._repository.Create(actionType));

            await this._unitOfWork.Commit();
        }

        public async Task<OperationTypeDTO> GetActionTypeById(long id)
        {
            OperationType? actionType = await Task.Run(
                () => this._repository.Get(actionType => actionType.OperationTypeId == id && actionType.Enabled is true).FirstOrDefault());

            OperationTypeDTO actionTypeDTO = this._mapperB.Map<OperationTypeDTO>(actionType);

            return actionTypeDTO;
        }

        public async Task<IList<OperationTypeDTO>> GetAllActionTypes()
        {
            IList<OperationType> actionTypes = await Task.Run(
                () => this._repository.Get(actionType => actionType.Enabled is true).ToList());

            IList<OperationTypeDTO> actionTypeDTOs = this._mapperB.Map<IList<OperationTypeDTO>>(actionTypes);

            return actionTypeDTOs;
        }

        public async Task SetActionTypeEnabled(long id, bool enabled)
        {
            OperationType? actionType = await Task.Run(
                () => this._repository.Get(actionType => actionType.OperationTypeId == id && actionType.Enabled is true).FirstOrDefault());

            if (actionType is null)
            {
                throw new OperationTypeNotFoundException($"Тип операции с идентификатором {id} не найден");
            }

            actionType.SetEnabled(enabled);

            await Task.Run(() => this._repository.Update(actionType));

            await this._unitOfWork.Commit();
        }
    }
}
