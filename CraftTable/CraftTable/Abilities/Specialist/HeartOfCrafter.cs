using CraftTable.Attributes;
using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities.Specialist
{
    [AbilityXivDb(Crafter.All, 100186)]
    [AbilityDescriptor("Heart of the Crafter", Crafter.All, 45, false, Category.Specialist, 0)]
    public class HeartOfCrafter : Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
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