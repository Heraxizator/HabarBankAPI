using Access.Application.DTOs.Base;
using Access.Application.DTOs.Requests;
using Access.Application.DTOs.Responses;
using Access.Application.Interfaces;
using Access.Domain.Entities;
using Common.Abstracts;
using Common.DTOs;
using Common.Extensions;
using Common.Infrastructure.Abstracts;
using Common.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Users.Application.Mappers;
using Users.Domain.Entities;
using Users.Infrastructure.Repositories;

namespace Access.Application.Services;

public sealed class AccessService(IDbContext context) : IAccessService
{
    private readonly IGenericRepository<AccessToken> _tokenRepository = new GenericRepository<AccessToken>(context);
    private readonly IGenericRepository<User> _userRepository = new GenericRepository<User>(context);

    private Task<User?> GetUserByLoginAndPasswordAsync(string login, string password, CancellationToken cancellationToken)
    {
        return _userRepository.FirstOrDefaultAsync(
            x => x.Login == login && x.Password == EncryptExtension.EncryptSHA1(password) && x.DeletedAt == default, cancellationToken);
    }

    public async Task<AccessToken> AuthentificateAsync(string? login, string? password, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(login);
        ArgumentException.ThrowIfNullOrWhiteSpace(password);

        User? user = await GetUserByLoginAndPasswordAsync(login, password, cancellationToken);

        ArgumentNullException.ThrowIfNull(user);

        AccessToken accessToken = new()
        {
            UserId = user.Id,
            Token = HashExtension.GenerateHash(),
            ExpiredAt = DateTimeOffset.Now.AddMonths(6),
            CreatedAt = DateTimeOffset.Now,
        };

        return await _tokenRepository.CreateAsync(accessToken, cancellationToken);
    }

    public async Task<UserBodyWithotPassword> IdentifyAsync(string token, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(token);

        AccessToken? accessToken = await _tokenRepository.FirstOrDefaultAsync(
           x => x.Token == token && x.ExpiredAt > DateTime.Now && x.DeletedAt == default, cancellationToken);

        ArgumentNullException.ThrowIfNull(accessToken);

        User? user = await _userRepository.FindByIdAsync(accessToken.UserId, cancellationToken);

        ArgumentNullException.ThrowIfNull(user);

        return MUser.GetBodyWithoutPassword(user);
    }
}
