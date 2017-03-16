using System.Collections.Generic;

namespace CrafterExperiment
{
    public class SameBuffEqualityComparer : IEqualityComparer<IBuff>
    {
        public bool Equals(IBuff x, IBuff y)
        {
            return x?.GetType() == y?.GetType();
        }

        public int GetHashCode(IBuff obj)
        {
            return obj?.GetType().Name.GetHashCode() ?? 0;
        }
    }
}