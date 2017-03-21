using CraftTable.Attributes;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 100107)]
    [AbilityDescriptor("Master's mend", Crafter.All, 92, false, Category.Durability, 0)]
    public class MastersMend : Ability 
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseCraftPoints(92);
            craftActions.RestoreDurability(30);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 92;
        }
    }
}