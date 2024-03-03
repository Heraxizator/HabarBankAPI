﻿using HabarBankAPI.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Specifications.Account
{
    public class PatronymicSpecification : Specification<string>
    {
        public override Expression<Func<string, bool>> ToExpression()
        {
            return patronymic => !string.IsNullOrEmpty(patronymic)
                && patronymic.Count(c => char.IsLetter(c)) == patronymic.Length
                && char.IsUpper(patronymic.FirstOrDefault())
                && patronymic.Length >= 5;
        }
    }
}
