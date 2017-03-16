using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities.Specialist
{
    public class NymeriasWheel : Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseCraftPoints(18);
            craftActions.RestoreDurability(0); //todo: calculate durability
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 18 && serviceState.BuffAccessor.GetBuff<WhistleBuff>() != null;
        }
    }
}