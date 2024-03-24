using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Entities.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.UnitTests.Domain.Entities
{
    public class SendingTests
    {
        [Theory]

        [InlineData(true)]

        public void CheckRunSendingThrowsIfCardsEmpty(bool throwed)
        {
            Sending sending = new();

            Exception exception = Record.Exception(
                () => sending.RunSending());

            bool isThrowed = exception != null;

            Assert.True(isThrowed == throwed);
        }
    }
}
