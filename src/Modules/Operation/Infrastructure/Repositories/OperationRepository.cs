using Common.Abstracts;
using Common.Infrastructure.Repositories;
using Operations.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations.Infrastructure.Repositories;

public class OperationRepository(IDbContext context) : GenericRepository<Operation>(context)
{
}
