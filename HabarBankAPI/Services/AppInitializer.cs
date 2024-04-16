using HabarBankAPI.Application.Interfaces;
using HabarBankAPI.Application.Services;
using HabarBankAPI.Domain.Entities.Security;
using HabarBankAPI.Infrastructure.Repositories;
using HabarBankAPI.Infrastructure.Share;
using HabarBankAPI.Infrastructure.Uow;

namespace HabarBankAPI.Web.Services
{
    public static class AppInitializer
    {
        public static void Init()
        {
            SecurityDbContext securityDbContext = new();

            GenericRepository<Security> securityRepository = new(securityDbContext);

            SecurityUnitOfWork securityUnitOfWork = new(securityDbContext);

            ServiceLocator.Instance.Register<ISecurityService>(new SecurityService(securityRepository, securityUnitOfWork));
        }
    }
}
