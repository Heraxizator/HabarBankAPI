﻿using HabarBankAPI.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Specifications.CardVariant
{
    public class CardVariantNameSpecification : Specification<string>
    {
        public override Expression<Func<string, bool>> ToExpression()
        {
            return name => !string.IsNullOrEmpty(name) && name.Length >= 5;
        }
    }
}
