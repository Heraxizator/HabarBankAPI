using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.UnitTests.Domain.Entities
{
    public class AdminTests
    {
        [Theory]

        [InlineData("", "", "", "", "", "", true)]
        [InlineData("Aleksandr", "x45hj346567", "+89142107908", "Александр", "Александров", "Александрович", false)]
        public void CheckSetProfileMethod(
            string login, string password, string phone, string name, string surname, string patronymic, bool throwed)
        {
            Admin admin = new ();

            Exception exception = Record.Exception(
                () => admin.SetAccountProfile(login, password, phone, name, surname, patronymic));

            bool isNotNull = exception != null;

            Assert.True(isNotNull == throwed);
        }
    }
}
