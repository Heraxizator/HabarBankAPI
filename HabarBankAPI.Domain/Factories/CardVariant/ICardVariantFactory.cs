using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Factories
{
    public interface ICardVariantFactory : IFactory<CardVariant>
    {
        ICardVariantFactory WithName(string name);
        ICardVariantFactory WithText(string text);
        ICardVariantFactory WithPercentage(int percentage);
        ICardVariantFactory WithCardType(CardType? cardType);
        ICardVariantFactory WithUserLevel(UserLevel? userLevel);
    }
}
