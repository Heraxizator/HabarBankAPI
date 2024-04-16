using HabarBankAPI.Domain.Share;

namespace HabarBankAPI.Web.Services
{
    public class ServiceLocator
    {
        private static ServiceLocator locator = null;

        public static ServiceLocator Instance
        {
            get
            {
                if (locator == null)
                {
                    locator = new ServiceLocator();
                }

                return locator;
            }
        }

        private ServiceLocator()
        {
        }

        private readonly Dictionary<Type, object> registry = new();

        public void Register<T>(T serviceInstance)
        {
            registry[typeof(T)] = serviceInstance;
        }

        public T GetService<T>()
        {
            T serviceInstance = (T)registry[typeof(T)];
            return serviceInstance;
        }
    }
}
