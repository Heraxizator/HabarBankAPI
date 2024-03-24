using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.UnitTests.Domain.Factories;

public class CardVariantFactoryTests
{
    [Fact]

    public void BuildShouldThrowIfCardVariantNameIsNull()
    {
        CardVariantFactory cardVariantFactory = new();

        Exception exception = Record.Exception(() => 
        {
            CardVariant cardVariant = cardVariantFactory
            .WithName(string.Empty)
            .WithText("Text about card variant")
            .WithCardType(new CardType())
            .WithUserLevel(new UserLevel())
            .WithPercentage(10)
            .Build();
        });

        Assert.True(exception is not null);
    }

    [Fact]

    public void BuildShouldThrowIfCardVariantTextIsNull()
    {
        CardVariantFactory cardVariantFactory = new();

        Exception exception = Record.Exception(() =>
        {
            CardVariant cardVariant = cardVariantFactory
            .WithName("Name of card variant")
            .WithText(string.Empty)
            .WithCardType(new CardType())
            .WithUserLevel(new UserLevel())
            .WithPercentage(10)
            .Build();
        });

        Assert.True(exception is not null);
    }

    [Fact]

    public void BuildShouldThrowIfCardTypeIsNull()
    {
        CardVariantFactory cardVariantFactory = new();

        Exception exception = Record.Exception(() =>
        {
            CardVariant cardVariant = cardVariantFactory
            .WithName("Name of card variant")
            .WithText("Text of card variant")
            .WithCardType(null)
            .WithUserLevel(new UserLevel())
            .WithPercentage(10)
            .Build();
        });

        Assert.True(exception is not null);
    }

    [Fact]

    public void BuildShouldThrowIfUserLevelIsNull()
    {
        CardVariantFactory cardVariantFactory = new();

        Exception exception = Record.Exception(() =>
        {
            CardVariant cardVariant = cardVariantFactory
            .WithName("Name of card variant")
            .WithText("Text of card variant")
            .WithCardType(new CardType())
            .WithUserLevel(null)
            .WithPercentage(10)
            .Build();
        });

        Assert.True(exception is not null);
    }

    [Fact]

    public void BuildShouldThrowIfPercentageIsNull()
    {
        CardVariantFactory cardVariantFactory = new();

        Exception exception = Record.Exception(() =>
        {
            CardVariant cardVariant = cardVariantFactory
            .WithName("Name of card variant")
            .WithText("Text of card variant")
            .WithCardType(new CardType())
            .WithUserLevel(new UserLevel())
            .WithPercentage(-1)
            .Build();
        });

        Assert.True(exception is not null);
    }

    [Fact]

    public void BuildShouldCreateIfCorrect()
    {
        CardVariantFactory cardVariantFactory = new();

        Exception exception = Record.Exception(() =>
        {
            CardVariant cardVariant = cardVariantFactory
            .WithName("Name of card variant")
            .WithText("Text of card variant")
            .WithCardType(new CardType())
            .WithUserLevel(new UserLevel())
            .WithPercentage(10)
            .Build();
        });

        Assert.True(exception is null);
    }
}

