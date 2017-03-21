using CraftTable.Attributes;
using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 281)]
    [AbilityDescriptor("Steady hand II", Crafter.All, 25, false, Category.Buffs, 2)]
    public class SteadyHand2 : Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseCraftPoints(25);
            craftActions.ApplyBuff(new SteadyHand2Buff(30));
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 25;
        }
    }
}