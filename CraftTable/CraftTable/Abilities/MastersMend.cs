using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    public class MastersMend : Ability 
    {
        public override void Execute(ICraftActions craftActions)
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