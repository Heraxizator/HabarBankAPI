using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Entities.Transfer;
using HabarBankAPI.Domain.Factories.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.UnitTests.Domain.Factories;

public class TransferFactoryTests
{
    [Fact]

    public void BuildShouldThrowIfSenderIsNull()
    {
        TransferFactory transferFactory = new();

        Exception exception = Record.Exception(() =>
        {
            Sending sending = transferFactory
                .WithCardSender(null)
                .WithCardRecipient(new Card())
                .WithOperationType(new OperationType())
                .WithRublesCount(1000)
                .Build();
        });

        Assert.True(exception is not null);
    }

    [Fact]

    public void BuildShouldThrowIfRecipientIsNull()
    {
        TransferFactory transferFactory = new();

        Exception exception = Record.Exception(() =>
        {
            Sending sending = transferFactory
                .WithCardSender(new Card())
                .WithCardRecipient(null)
                .WithOperationType(new OperationType())
                .WithRublesCount(1000)
                .Build();
        });

        Assert.True(exception is not null);
    }

    [Fact]

    public void BuildShouldThrowIfOperationTypeIsNull()
    {
        TransferFactory transferFactory = new();

        Exception exception = Record.Exception(() =>
        {
            Sending sending = transferFactory
                .WithCardSender(new Card())
                .WithCardRecipient(new Card())
                .WithOperationType(null)
                .WithRublesCount(1000)
                .Build();
        });

        Assert.True(exception is not null);
    }

    [Fact]

    public void BuildShouldThrowIfRublesCountIsNull()
    {
        TransferFactory transferFactory = new();

        Exception exception = Record.Exception(() =>
        {
            Sending sending = transferFactory
                .WithCardSender(new Card())
                .WithCardRecipient(new Card())
                .WithOperationType(new OperationType())
                .WithRublesCount(-1)
                .Build();
        });

        Assert.True(exception is not null);
    }

    [Fact]

    public void BuildShouldThrowIfObjectsEmpty()
    {
        TransferFactory transferFactory = new();

        Exception exception = Record.Exception(() =>
        {
            Sending sending = transferFactory
                .WithCardSender(new Card())
                .WithCardRecipient(new Card())
                .WithOperationType(new OperationType())
                .WithRublesCount(10)
                .Build();
        });

        Assert.True(exception is not null);
    }
}
