using System;
using System.Collections.Generic;
using CraftTable.Buffs;

namespace CraftTable
{
    public class SameBuffEqualityComparer : IEqualityComparer<IBuff>
    {
        public bool Equals(IBuff x, IBuff y)
        {
            if (x == null || y == null) return false;
            var typesSame = x.GetType() == y.GetType();
            if (!typesSame)
            {
                var vx = ValidBaseType(x.GetType());
                var vy = ValidBaseType(y.GetType());

                if (vx == null || vy == null) return false;
                return vx == vy;
            }
            return true;
        }

        private Type ValidBaseType(Type type)
        {
            if (type.BaseType.IsInterface) return null;
            if (type.BaseType == typeof(object)) return null;
            if (type.BaseType == typeof(StepsBasedBuff)) return null;
            return type.BaseType;
        }

        public int GetHashCode(IBuff obj)
        {
            return obj?.GetType().Name.GetHashCode() ?? 0;
        }
    }
}