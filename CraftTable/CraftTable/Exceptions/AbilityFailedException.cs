using System;

namespace CraftTable.Exceptions
{
    public class AbilityFailedException : Exception
    {
        public AbilityFailedException(int chance)
        {
            this.Chance = chance;
        }

        public int Chance { get; set; }
    }
}