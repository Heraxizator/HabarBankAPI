using HabarBankAPI.Domain.Entities.Admin;
using HabarBankAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabarBankAPI.Domain.Factories;
using HabarBankAPI.Domain.Entities;

namespace HabarBankAPI.UnitTests.Domain.Factories
{
    public class UserFactoryTests
    {
        [Fact]

        public void BuildShouldThrowIfLoginIsNull()
        {
            UserFactory userFactory = new();

            Exception exception = Record.Exception(() =>
                userFactory
                    .WithLogin(string.Empty)
                    .WithPassword("testpassword")
                    .WithPhone("+89142107908")
                    .WithName("Name")
                    .WithSurname("Surname")
                    .WithPatronymic("Patronymic")
                    .WithAccountLevel(new UserLevel())
                    .Build());

            Assert.True(exception is not null);
        }

        [Fact]

        public void BuildShouldThrowIfPasswordIsNull()
        {
            UserFactory userFactory = new();

            Exception exception = Record.Exception(() =>
                userFactory
                    .WithLogin("test login")
                    .WithPassword(string.Empty)
                    .WithPhone("+89142107908")
                    .WithName("Name")
                    .WithSurname("Surname")
                    .WithPatronymic("Patronymic")
                    .WithAccountLevel(new UserLevel())
                    .Build());

            Assert.True(exception is not null);
        }

        [Fact]

        public void BuildShouldThrowIfPhoneIsNull()
        {
            UserFactory userFactory = new();

            Exception exception = Record.Exception(() =>
                userFactory
                    .WithLogin("test login")
                    .WithPassword("testpassword")
                    .WithPhone(string.Empty)
                    .WithName("Name")
                    .WithSurname("Surname")
                    .WithPatronymic("Patronymic")
                    .WithAccountLevel(new UserLevel())
                    .Build());

            Assert.True(exception is not null);
        }

        [Fact]

        public void BuildShouldThrowIfNameIsNull()
        {
            UserFactory userFactory = new();

            Exception exception = Record.Exception(() =>
                userFactory
                    .WithLogin("test login")
                    .WithPassword("testpassword")
                    .WithPhone("+89142107908")
                    .WithName(string.Empty)
                    .WithSurname("Surname")
                    .WithPatronymic("Patronymic")
                    .WithAccountLevel(new UserLevel())
                    .Build());

            Assert.True(exception is not null);
        }

        [Fact]

        public void BuildShouldThrowIfSurnameIsNull()
        {
            UserFactory userFactory = new();

            Exception exception = Record.Exception(() =>
                userFactory
                    .WithLogin("test login")
                    .WithPassword("testpassword")
                    .WithPhone("+89142107908")
                    .WithName("Name")
                    .WithSurname(string.Empty)
                    .WithPatronymic("Patronymic")
                    .WithAccountLevel(new UserLevel())
                    .Build());

            Assert.True(exception is not null);
        }

        [Fact]

        public void BuildShouldThrowIfPatronymicIsNull()
        {
            UserFactory userFactory = new();

            Exception exception = Record.Exception(() =>
                userFactory
                    .WithLogin("test login")
                    .WithPassword("testpassword")
                    .WithPhone("+89142107908")
                    .WithName("Name")
                    .WithSurname("Surname")
                    .WithPatronymic(string.Empty)
                    .WithAccountLevel(new UserLevel())
                    .Build());

            Assert.True(exception is not null);
        }

        [Fact]

        public void BuildShouldThrowIfUserLevelIsNull()
        {
            UserFactory userFactory = new();

            Exception exception = Record.Exception(() =>
                userFactory
                    .WithLogin("test login")
                    .WithPassword("testpassword")
                    .WithPhone("+89142107908")
                    .WithName("Name")
                    .WithSurname("Surname")
                    .WithPatronymic("Patronymic")
                    .WithAccountLevel(null)
                    .Build());

            Assert.True(exception is not null);
        }

        [Fact]

        public void BuildShouldCreateWithCorrectParameters()
        {
            UserFactory userFactory = new();

            Exception exception = Record.Exception(() =>
                userFactory
                    .WithLogin("test login")
                    .WithPassword("testpassword")
                    .WithPhone("+89142107908")
                    .WithName("Name")
                    .WithSurname("Surname")
                    .WithPatronymic("Patronymic")
                    .WithAccountLevel(new UserLevel())
                    .Build());

            Assert.True(exception is null);
        }
    }
}
