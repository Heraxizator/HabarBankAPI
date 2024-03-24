using AutoMapper;
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

        internal void UnitService()
        {
            ApplicationDbContext context = new();

            GenericRepository<Admin> adminRepository = new(context);

            UnitOfWork unitOfWork = new(context);

            Mapper mapper = AbstractMapper<Admin, AdminDTO>.MapperA;

            this._service = new AdminService(mapper, adminRepository, unitOfWork);
        }

        [Fact]

        public async void GetAccountsListTest()
        {
            UnitService();

            if (this._service is null)
            {
                return;
            }

            Exception exception = await Record.ExceptionAsync(async () =>
            {
                await this._service.GetAccountsList(10);
            });

            Assert.True(exception is null);
        }

        [Fact]

        public async void GetAccountById()
        {
            UnitService();

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
    }
}
