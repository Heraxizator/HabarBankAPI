using HabarBankAPI.Domain.Entities;
using HabarBankAPI.Domain.Exceptions.AccountLevel;
using HabarBankAPI.Domain.Specifications.AccountLevel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Factories
{
    public class UserLevelFactory : IUserLevelFactory
    {
        private string UserLevelName = string.Empty;

        public UserLevel Build()
        {
            UserLevelNameSpecification specification = new();

            if (specification.IsSatisfiedBy(this.UserLevelName) is false)
            {
                throw new BadUserLevelNameException("Имя уровня пользователя не соответствует требованиям");
            }

            return new UserLevel(this.UserLevelName);
        }

        public IUserLevelFactory WithName(string name)
        {
            this.UserLevelName = name;
            return this;
        }
    }
}
