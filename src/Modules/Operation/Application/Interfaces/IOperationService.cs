using Operations.Application.DTOs.Base;
using Operations.Application.DTOs.Requests;
using Operations.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations.Application.Interfaces;

public interface IOperationService
{
    Task<Operation> CreateAsync(CreateOperationRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<Operation>> GetAsync(GetOperationsRequest request, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateOperationRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(DeleteOperationRequest request, CancellationToken cancellationToken);
}
