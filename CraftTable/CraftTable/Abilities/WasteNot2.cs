using CraftTable.Attributes;
using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 285)]
    [AbilityDescriptor("Waste not 2", Crafter.Leatherworker, 98, true, Category.Durability, 3)]
    public class WasteNot2 : Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseCraftPoints(98);
            craftActions.ApplyBuff(new WasteNot2Buff(8));
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 98;
        }
    }
}