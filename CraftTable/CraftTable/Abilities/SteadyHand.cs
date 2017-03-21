using CraftTable.Attributes;
using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 251)]
    [AbilityDescriptor("Steady hand", Crafter.All, 22, false, Category.Buffs, 1)]
    public class SteadyHand : Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseCraftPoints(22);
            craftActions.ApplyBuff(new SteadyHandBuff(20));
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft>=22;
        }
    }
}