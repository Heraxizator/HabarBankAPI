using HabarBankAPI.Domain.Entities.Card;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Factories
{
    public interface ICardFactory : IFactory<Card>
    {
        ICardFactory WithCardVariantId(long cardVariantId);
        ICardFactory WithRublesCount(int rublesCount);
        ICardFactory WithImagePath(string imagePath);
    }
}
