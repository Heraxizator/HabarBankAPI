using HabarBankAPI.Domain.Entities;
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
        ICardFactory WithCardVariant(CardVariant? cardVariant);
        ICardFactory WithCardUser(User? user);
        ICardFactory WithRublesCount(int rublesCount);
        ICardFactory WithImagePath(string imagePath);
    }
}
