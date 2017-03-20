using CraftTable.Attributes;
using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities.Specialist
{
    [AbilityXivDb(Crafter.All, 100176)]
    public class Satisfaction : Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
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