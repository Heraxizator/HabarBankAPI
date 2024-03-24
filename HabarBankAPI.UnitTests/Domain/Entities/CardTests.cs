using HabarBankAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.UnitTests.Domain.Entities
{
    public class CardTests
    {
        [Fact]
        public void CheckSetPercentageIfVariantIsNull()
        {
            Card card = new();

            Exception exception = Record.Exception(() => card.SetPercentages());

            Assert.True(exception != null);
        }

        [Theory]

        [InlineData(-1000, true)]
        [InlineData(0, true)]
        [InlineData(1000, false)]
        public void CheckIncreaseRublesCountWorked(int rublesCount, bool throwed)
        {
            Card card = new();

            Exception exception = Record.Exception(
                () => card.IncreaseRublesCount(rublesCount));

            bool isThrowed = exception != null;

            Assert.True(isThrowed == throwed);
        }

        [Theory]

        [InlineData(-1000, true)]
        [InlineData(0, true)]
        [InlineData(1000, false)]

        public void CheckIfDecreaseRublesCountWorked(int rublesCount, bool throwed)
        {
            Card card = new();

            Exception exception = Record.Exception(
                () => card.DecreaseRublesCount(rublesCount));

            bool isThrowed = exception != null;

            Assert.True(isThrowed == throwed);
        }
    }
}
