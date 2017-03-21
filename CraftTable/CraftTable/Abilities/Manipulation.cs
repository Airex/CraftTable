using CraftTable.Attributes;
using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 278)]
    [AbilityDescriptor("Manipulation", Crafter.GoldSmith, 88, true, Category.Durability, 4)]
    public class Manipulation:Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseCraftPoints(88);
            craftActions.ApplyBuff(new ManipulationBuff());
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 88;
        }

    }
}