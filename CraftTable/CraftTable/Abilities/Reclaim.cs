using CraftTable.Attributes;
using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 287)]
    [AbilityDescriptor("Advanced touch", Crafter.Culinarian, 55, true, Category.Buffs, 10)]
    public class Reclaim : Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseCraftPoints(55);
            craftActions.ApplyBuff(new ReclaimBuff());
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 55;
        }
    }
}