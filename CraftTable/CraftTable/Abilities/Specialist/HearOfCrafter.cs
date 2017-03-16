using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities.Specialist
{
    public class HearOfCrafter : Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseCraftPoints(45);
            craftActions.ApplyBuff(new HeartOfCrafterBuff());
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 45 && serviceState.BuffAccessor.GetBuff<HeartOfCrafterBuff>() == null;
        }
    }
}