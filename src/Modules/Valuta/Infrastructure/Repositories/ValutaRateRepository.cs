using Common.Abstracts;
using Common.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valutas.Domain.Entities;

namespace Valutas.Infrastructure.Repositories;

public class ValutaRateRepository(IDbContext context) : GenericRepository<Valuta>(context)
{
}
