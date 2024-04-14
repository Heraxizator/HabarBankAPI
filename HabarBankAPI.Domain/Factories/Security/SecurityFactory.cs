using HabarBankAPI.Domain.Entities.Security;
using HabarBankAPI.Domain.Exceptions.Security;
using HabarBankAPI.Domain.Specifications.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Factories
{
    public class SecurityFactory : ISecurityFactory
    {
        private string _email = string.Empty;

        public Security Build()
        {
            EmailSpecification emailSpecification = new();

            if (emailSpecification.IsSatisfiedBy(this._email) is false)
            {
                throw new InvalidEmailException("Email не соответствует требованиям");
            }

            Security security = new
            (
                this._email,
                string.Empty
            );

            security.SetToken();

            return security;
        }

        public ISecurityFactory WithEmail(string email)
        {
            this._email = email;
            return this;
        }
    }
}
