using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities.Specialist
{
    public class Satisfaction : Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.RestoreCraftPoints(15);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            var whistleBuff = serviceState.BuffAccessor.GetBuff<WhistleBuff>();
            return whistleBuff != null && whistleBuff.Stacks % 3 == 0;
        }
    }
}