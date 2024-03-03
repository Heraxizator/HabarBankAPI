﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabarBankAPI.Domain.Share
{
    public abstract class ValueObject
    {
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            return !(left is null ^ right is null) && left?.Equals(right!) != false;
        }

        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !EqualOperator(left, right);
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            ValueObject other = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}