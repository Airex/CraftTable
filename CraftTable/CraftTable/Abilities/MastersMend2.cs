using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    public class MastersMend2 : Ability
    {
        public override void Execute(ICraftActions craftActions)
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