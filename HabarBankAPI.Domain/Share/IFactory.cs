using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Share
{
    public interface IFactory<TEntity>  where TEntity : IAggregateRoot
    {
        TEntity Build();
    }
}
