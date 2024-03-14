﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Exceptions.CardVariant
{
    public class BadVariantTextException : Exception
    {
        public BadVariantTextException(string? message) : base(message)
        {
        }
    }
}
