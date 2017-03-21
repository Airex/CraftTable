using CraftTable.Attributes;
using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 286)]
    [AbilityDescriptor("Comfort zone", Crafter.Alchemist, 66, true, Category.CP, 0)]
    public class ComfortZone : Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseCraftPoints(66);
            craftActions.ApplyBuff(new ComfortZoneBuff());
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 66;
        }
    }
}