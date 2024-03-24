using HabarBankAPI.Domain;
using HabarBankAPI.Domain.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.UnitTests.Domain.Factories
{
    public class AdminFactoryTests
    {
        [Fact]

        public void BuildShouldThrowIfLoginIsNull()
        {
            AdminFactory adminFactory = new();

            Exception exception = Record.Exception(() =>
                adminFactory
                    .WithLogin(string.Empty)
                    .WithPassword("testpassword")
                    .WithPhone("+89142107908")
                    .WithName("name")
                    .WithSurname("surname")
                    .WithPatronymic("patronymic")
                    .Build());

            Assert.True(exception is not null);
        }

        [Fact]

        public void BuildShouldThrowIfPasswordIsNull()
        {
            AdminFactory adminFactory = new();

            Exception exception = Record.Exception(() =>
                adminFactory
                    .WithLogin("test login")
                    .WithPassword(string.Empty)
                    .WithPhone("+89142107908")
                    .WithName("name")
                    .WithSurname("surname")
                    .WithPatronymic("patronymic")
                    .Build());

            Assert.True(exception is not null);
        }

        [Fact]

        public void BuildShouldThrowIfPhoneIsNull()
        {
            AdminFactory adminFactory = new();

            Exception exception = Record.Exception(() =>
                adminFactory
                    .WithLogin("test login")
                    .WithPassword("testpassword")
                    .WithPhone(string.Empty)
                    .WithName("name")
                    .WithSurname("surname")
                    .WithPatronymic("patronymic")
                    .Build());

            Assert.True(exception is not null);
        }

        [Fact]

        public void BuildShouldThrowIfNameIsNull()
        {
            AdminFactory adminFactory = new();

            Exception exception = Record.Exception(() =>
                adminFactory
                    .WithLogin("test login")
                    .WithPassword("testpassword")
                    .WithPhone("+89142107908")
                    .WithName(string.Empty)
                    .WithSurname("surname")
                    .WithPatronymic("patronymic")
                    .Build());

            Assert.True(exception is not null);
        }

        [Fact]

        public void BuildShouldThrowIfSurnameIsNull()
        {
            AdminFactory adminFactory = new();

            Exception exception = Record.Exception(() =>
                adminFactory
                    .WithLogin("test login")
                    .WithPassword("testpassword")
                    .WithPhone("+89142107908")
                    .WithName("name")
                    .WithSurname(string.Empty)
                    .WithPatronymic("patronymic")
                    .Build());

            Assert.True(exception is not null);
        }

        [Fact]

        public void BuildShouldThrowIfPatronymicIsNull()
        {
            AdminFactory adminFactory = new();

            Exception exception = Record.Exception(() =>
                adminFactory
                    .WithLogin("test login")
                    .WithPassword("testpassword")
                    .WithPhone("+89142107908")
                    .WithName("name")
                    .WithSurname("surname")
                    .WithPatronymic(string.Empty)
                    .Build());

            Assert.True(exception is not null);
        }

        [Fact]

        public void BuildShouldCreateWithCorrectParameters()
        {
            AdminFactory adminFactory = new();

            Admin admin = adminFactory
                .WithLogin("sukhikh")
                .WithPassword("3ty57tghmk546")
                .WithPhone("+89142107904")
                .WithName("Максим")
                .WithSurname("Сухих")
                .WithPatronymic("Алексеевич")
                .Build();

            Assert.True(admin is not null);
        }
    }
}
