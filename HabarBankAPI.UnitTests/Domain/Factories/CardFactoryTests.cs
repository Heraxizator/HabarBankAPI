using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Factories;

namespace HabarBankAPI.UnitTests.Domain.Factories
{
    public class CardFactoryTests
    {
        [Fact]

        public void BuildShouldThrowIfImagePathIsNull()
        {
            CardFactory cardFactory = new();

            Exception exception = Record.Exception(() => cardFactory
                .WithImagePath(string.Empty)
                .WithCardUser(new HabarBankAPI.Domain.Entities.User())
                .WithCardVariant(new CardVariant())
                .WithRublesCount(1000)
                .Build());

            Assert.True(exception is not null);
        }

        [Fact]

        public void BuildShouldThrowIfUserIsNull() 
        {
            CardFactory cardFactory = new();

            Exception exception = Record.Exception(() => cardFactory
                .WithImagePath("https://google.com")
                .WithCardUser(null)
                .WithCardVariant(new CardVariant())
                .WithRublesCount(1000)
                .Build());

            Assert.True(exception is not null);
        }

        [Fact]

        public void BuildShouldThrowIfVariantIsNull()
        {
            CardFactory cardFactory = new();

            Exception exception = Record.Exception(() => cardFactory
                .WithImagePath("https://google.com")
                .WithCardUser(new HabarBankAPI.Domain.Entities.User())
                .WithCardVariant(null)
                .WithRublesCount(1000)
                .Build());

            Assert.True(exception is not null);
        }

        [Fact]

        public void BuildShouldThrowIfRublesCountIsNull()
        {
            CardFactory cardFactory = new();

            Exception exception = Record.Exception(() => cardFactory
                .WithImagePath("https://google.com")
                .WithCardUser(new HabarBankAPI.Domain.Entities.User())
                .WithCardVariant(new CardVariant())
                .WithRublesCount(0)
                .Build());

            Assert.True(exception is not null);
        }

        [Fact]

        public void BuildShouldCreateWithParameters()
        {
            CardFactory cardFactory = new();

            Card card = cardFactory
                .WithImagePath("https://google.com")
                .WithCardUser(new HabarBankAPI.Domain.Entities.User())
                .WithCardVariant(new CardVariant())
                .WithRublesCount(1000)
                .Build();

            Assert.True(card is not null);
        }
    }
}
