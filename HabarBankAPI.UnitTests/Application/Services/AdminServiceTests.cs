using AutoMapper;
using HabarBankAPI.Application.DTO.Accounts;
using HabarBankAPI.Application.DTO.Admins;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Data;
using HabarBankAPI.Domain.Abstractions.Mappers;
using HabarBankAPI.Domain.Abstractions.Repositories;
using HabarBankAPI.Domain.Entities.Admin;
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.UnitTests.Application.Services
{
    public class AdminServiceTests
    {
        private AdminService? _service { get; set; }

        public AdminServiceTests()
        {
            UnitService();
        }

        internal void UnitService()
        {
            ApplicationDbContext context = new();

            GenericRepository<Admin> adminRepository = new(context);

            AppUnitOfWork unitOfWork = new(context);

            Mapper mapper = AbstractMapper<Admin, AdminDTO>.MapperA;

            this._service = new AdminService(mapper, adminRepository, unitOfWork);
        }

        [Fact]

        public async void GetAccountsListTest()
        {
            if (this._service is null)
            {
                return;
            }

            Exception exception = await Record.ExceptionAsync(
                async () => await this._service.GetAccountsList(10));

            Assert.True(exception is null);
        }

        [Fact]

        public async void GetAccountById()
        {
            if (this._service is null)
            {
                return;
            }

            Exception exception = await Record.ExceptionAsync(async () =>
            {
                AdminDTO adminDTO = await this._service.GetAccountById(1);
            });

            Assert.True(exception is null);
        }

        [Fact]

        public async void CreateAdminAccountTest()
        {
            // Arrange

            if (this._service is null)
            {
                return;
            }

            AdminDTO adminDTO = new()
            {
                AdminId = -1,
                AccountLogin = "loginxxx",
                AccountName = "Иван",
                AccountSurname = "Иванов",
                AccountPatronymic = "Иванович",
                AccountPassword = "12345678",
                AccountPhone = "+89142107904",
                Enabled = false
            };

            // Action

            Exception exception = await Record.ExceptionAsync(async () =>
                await this._service.CreateAdminAccount(adminDTO)
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

            AdminDTO adminDTO = new()
            {
                AdminId = -1,
                AccountLogin = "loginxxx",
                AccountName = "Иван",
                AccountSurname = "Иванов",
                AccountPatronymic = "Иванович",
                AccountPassword = "12345678",
                AccountPhone = "+89142107904",
                Enabled = true
            };

            await this._service.CreateAdminAccount(adminDTO);

            // Action

            AdminDTO admin = await this._service.GetAuthTokenByData("Test123", "12345678");

            await this._service.EditAccountEnabled(admin.AdminId, false);

            // Assert

            Assert.NotNull(admin);
        }

        [Fact]

        public async void EditAccoutEnabledTest()
        {
            // Arrange

            if (this._service is null)
            {
                return;
            }

            AdminDTO adminDTO = new()
            {
                AdminId = -1,
                AccountLogin = "loginxxx",
                AccountName = "Иван",
                AccountSurname = "Иванов",
                AccountPatronymic = "Иванович",
                AccountPassword = "12345678",
                AccountPhone = "+89142107904",
                Enabled = true
            };

            await this._service.CreateAdminAccount(adminDTO);

            AdminDTO admin = await this._service.GetAuthTokenByData("Test123", "12345678");

            // Action

            Exception exception = await Record.ExceptionAsync(async () =>
                await this._service.EditAccountEnabled(adminDTO.AdminId, false)
            );

            // Assert

            Assert.True(exception is null);
        }
    }
}
