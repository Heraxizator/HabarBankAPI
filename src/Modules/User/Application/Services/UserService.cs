using Common.Abstracts;
using Common.Extensions;
using Common.Infrastructure.Abstracts;
using Common.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Users.Application.DTOs.Requests;
using Users.Application.Interfaces;
using Users.Application.Mappers;
using Users.Domain.Entities;

namespace Users.Application.Services;

public class UserService(IDbContext context) : IUserService
{
    private readonly IGenericRepository<User> _repository = new GenericRepository<User>(context);

    public async Task<User?> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        User user = MUser.GetModelWithPassword(request.Body);

        user.Password = EncryptExtension.EncryptSHA1(user.Password!);

        return await _repository.CreateAsync(user, cancellationToken);
    }

    public async Task DeleteAsync(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        User? user = await _repository.FindByIdAsync(request.Id, cancellationToken);

        ArgumentNullException.ThrowIfNull(user);

        await _repository.RemoveAsync(user, cancellationToken);
    }

    public async Task<IEnumerable<User>> SelectAsync(GetUserRequest request, CancellationToken cancellationToken)
    {
        return await _repository.GetAsync(x => true, request.PerInPage, request.Page, cancellationToken);
    }

    public async Task UpdateAsync(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        User user = MUser.GetModelWithoutPassword(request.Body);

        await _repository.UpdateAsync(user, cancellationToken);
    }
}