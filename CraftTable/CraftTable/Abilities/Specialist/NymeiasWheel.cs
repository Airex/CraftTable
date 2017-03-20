using CraftTable.Attributes;
using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities.Specialist
{
    [AbilityXivDb(Crafter.All, 100160)]
    public class NymeiasWheel : Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseCraftPoints(18);
            var durability = craftActions.CalculateDependency((a, b, c) => (int)b.MapNymeriasWheelStacks(a.GetBuff<WhistleBuff>().Stacks));
            craftActions.RestoreDurability(durability);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 18 && serviceState.BuffAccessor.GetBuff<WhistleBuff>() != null;
        }
    }
}