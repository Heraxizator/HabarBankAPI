using HabarBankAPI.Controllers;
using HabarBankAPI.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.UnitTests.Web.Controllers
{
    public class AdminControllerTests
    {
        private AdminController? _adminController;

        private SecurityController? _securityController;

        [Fact]
        public async void GetAllAdminsTest()
        {
            // Arrange

            this._adminController = new AdminController();

            this._securityController = new SecurityController();

            await this._securityController.GenerateToken("i.i.ivanov@yandex.ru");

            string? token = (await this._securityController.GetToken("i.i.ivanov@yandex.ru")).Value;

            // Action

            Exception exception = await Record.ExceptionAsync(
                async () => await this._adminController.TakeUsers(1000, token));

            // Assert

            Assert.True(exception is null);
        }
    }
}
