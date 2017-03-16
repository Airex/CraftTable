using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    public class Observe : Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseCraftPoints(14);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 14;
        }
    }
}