using System;

namespace CraftTable
{
    public class ActionInfo
    {
        public ActionInfo(Type abilityType, Condition condition)
        {
            AbilityType = abilityType;
            Condition = condition;
        }

        public Type AbilityType { get; private set; }
        public Condition Condition { get; private set; }

    }
}