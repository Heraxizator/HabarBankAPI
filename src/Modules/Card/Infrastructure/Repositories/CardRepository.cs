using Cards.Domain.Entities;
using Common.Abstracts;
using Common.Infrastructure.Abstracts;
using Common.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards.Infrastructure.Repositories;

public class CardRepository(IDbContext context) : GenericRepository<Domain.Entities.Card>(context)
{
}
