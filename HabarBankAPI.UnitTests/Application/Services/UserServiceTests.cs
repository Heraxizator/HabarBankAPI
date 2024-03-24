using AutoMapper;
using HabarBankAPI.Application.DTO.Users;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = System.Action;

namespace HabarBankAPI.UnitTests.Application.Services
{
    public class UserServiceTests
    {
        private UserService? _service { get; set; }

        internal void UnitService()
        {
            Mapper mapper = AbstractMapper<User, UserDTO>.MapperA;

            ApplicationDbContext context = new();

            GenericRepository<User> userRepository = new(context);

            GenericRepository<UserLevel> levelsRepository = new(context);

            UnitOfWork unitOfWork = new(context);
        }

        [Fact]

        public async void GetAllUsersTest()
        {
            UnitService();

            if (this._service is null)
            {
                return;
            }

            Action action = async() => await this._service.GetAccountsList(10);

            Assert.ThrowsAny<Exception>(() => action);
        }
    }
}
