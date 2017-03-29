using CraftTable.Attributes;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 100113)]
    [AbilityDescriptor("Observe", Crafter.All, 14, false, Category.Other, 0)]
    public class Observe : Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseCraftPoints(14);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 14;
        }
    }
}