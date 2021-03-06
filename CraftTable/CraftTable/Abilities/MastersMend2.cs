using CraftTable.Attributes;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 100110)]
    [AbilityDescriptor("Master's mend II", Crafter.All, 160, false, Category.Durability, 1)]
    public class MastersMend2 : Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseCraftPoints(160);
            craftActions.RestoreDurability(60);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 160;
        }
    }
}