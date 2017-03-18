using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 100127)]
    public class ByregotsBrow:Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseCraftPoints(18);
            craftActions.UseDurability(10);
            craftActions.Touch(150);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 18 &&
                   serviceState.BuffAccessor.GetBuff<InnerQuietBuff>()?.Stacks >= 2 && serviceState.Condition.IsGoodOrExcellent();
        }
    }
}