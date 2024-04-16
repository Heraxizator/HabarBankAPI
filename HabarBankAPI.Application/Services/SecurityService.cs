using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Domain.Abstractions.Database;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities.Security;
using HabarBankAPI.Domain.Factories;
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Application.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IGenericRepository<Security> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SecurityService(IGenericRepository<Security> repository, IUnitOfWork unitOfWork) 
        {
            this._repository = repository;
            this._unitOfWork = unitOfWork;
        }

        public async Task GenerateToken(string email)
        {
            IList<Security> securities = this._repository.Get(x => x.Email == email).ToList();

            if (securities.Any())
            {
                throw new Exception("Токен для данного почтового ящика уже существует");
            }

            SecurityFactory securityFactory = new();

            Security security = securityFactory
                .WithEmail(email)
                .Build();

            this._repository.Create(security);

            await this._unitOfWork.Commit();
        }

        public async Task<string?> GetToken(string? email)
        {
            IList<Security> securities = this._repository.Get(x => x.Email == email).ToList();

            if (securities.Count > 1)
            {
                throw new Exception("Не может быть несколько токенов для одного почтового ящика");
            }

            if (securities.Any() is false)
            {
                throw new Exception("Токен для данного почтового ящика не найден");
            }

            return securities.FirstOrDefault()?.Token;
        }

        public async Task IsExists(string token)
        {
            IList<Security> securities = this._repository.Get(x => x.Token == token).ToList();

            if (securities.Any() is false)
            {
                throw new Exception("Аутентификация провалена");
            }
        }
    }
}
