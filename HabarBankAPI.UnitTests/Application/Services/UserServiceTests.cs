using AutoMapper;
using HabarBankAPI.Application.DTO.Admins;
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

        public UserServiceTests()
        {
            UnitService();
        }

        internal void UnitService()
        {
            Mapper mapper = AbstractMapper<User, UserDTO>.MapperA;

            ApplicationDbContext context = new();

            GenericRepository<User> userRepository = new(context);

            GenericRepository<UserLevel> levelsRepository = new(context);

            AppUnitOfWork unitOfWork = new(context);
        }

        [Fact]

        public void GetAllUsersTest()
        {
            if (this._service is null)
            {
                return;
            }

            Action action = async() => await this._service.GetAccountsList(10);

            Assert.ThrowsAny<Exception>(() => action);
        }

        [Fact]

        public async void CreateUserAccountTest()
        {
            // Arrange

            if (this._service is null)
            {
                return;
            }

            UserDTO userDTO = new()
            {
                UserId = -1,
                AccountLogin = "Test123",
                AccountName = "Иван",
                AccountSurname = "Иванов",
                AccountPatronymic = "Иванович",
                AccountPassword = "12345678",
                AccountPhone = "+89142107904",
                UserLevelId = 1,
                Enabled = false
            };

            // Action

            Exception exception = await Record.ExceptionAsync(async () =>
                await this._service.CreateUserAccount(userDTO)
            );

            // Assert

            Assert.True(exception is null);
        }

        [Fact]

        public async void GetAuthTokenByDataTest()
        {
            // Arrange

            if (this._service is null)
            {
                return;
            }

            UserDTO userDTO = new()
            {
                UserId = -1,
                AccountLogin = "Test123",
                AccountName = "Иван",
                AccountSurname = "Иванов",
                AccountPatronymic = "Иванович",
                AccountPassword = "12345678",
                AccountPhone = "+89142107904",
                UserLevelId = 1,
                Enabled = true
            };

            await this._service.CreateUserAccount(userDTO);

            // Action

            UserDTO user = await this._service.GetAuthTokenByData("Test123", "12345678");

            await this._service.EditAccountEnabled(user.UserId, false);

            // Assert

            Assert.NotNull(user);
        }

        [Fact]

        public async void EditAccoutEnabledTest()
        {
            // Arrange

            if (this._service is null)
            {
                return;
            }

            UserDTO userDTO = new()
            {
                UserId = -1,
                AccountLogin = "Test123",
                AccountName = "Иван",
                AccountSurname = "Иванов",
                AccountPatronymic = "Иванович",
                AccountPassword = "12345678",
                AccountPhone = "+89142107904",
                UserLevelId = 1,
                Enabled = true
            };

            await this._service.CreateUserAccount(userDTO);

            UserDTO user = await this._service.GetAuthTokenByData("Test123", "12345678");

            // Action

            Exception exception = await Record.ExceptionAsync(async () =>
                await this._service.EditAccountEnabled(user.UserId, false)
            );

            // Assert

            Assert.True(exception is null);
        }

    }
}
