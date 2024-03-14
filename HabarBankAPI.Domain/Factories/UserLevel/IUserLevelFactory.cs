using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Factories
{
    public interface IUserLevelFactory : IFactory<UserLevel>
    {
        IUserLevelFactory WithName(string name);
    }
}
