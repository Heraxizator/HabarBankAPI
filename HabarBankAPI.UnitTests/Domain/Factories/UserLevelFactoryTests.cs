using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.UnitTests.Domain.Factories;

public class UserLevelFactoryTests
{
    [Fact]

    public void BuildShouldThrowIfNameIsNull()
    {
        UserLevelFactory userLevelFactory = new();

        Exception exception = Record.Exception(() =>
        {
            UserLevel userLevel = userLevelFactory
                .WithName(string.Empty)
                .Build();
        });

        Assert.True(exception is not null);
    }

    [Fact]

    public void BuildShouldCreateWithCorrectParameters()
    {
        UserLevelFactory userLevelFactory = new();

        Exception exception = Record.Exception(() =>
        {
            UserLevel userLevel = userLevelFactory
                .WithName("name of user level")
                .Build();
        });

        Assert.True(exception is null);
    }
}
