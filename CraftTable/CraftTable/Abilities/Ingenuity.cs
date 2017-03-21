using CraftTable.Attributes;
using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 277)]
    [AbilityDescriptor("Ingenuity", Crafter.BlackSmith, 24, true, Category.Buffs, 3)]
    public class Ingenuity : Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseCraftPoints(24);
            craftActions.ApplyBuff(new IngenuityBuff());
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 24;
        }
    }
}