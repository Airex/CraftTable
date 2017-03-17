using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    public class TricksOfTheTrade:Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.RestoreCraftPoints(20);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.Condition.IsGoodOrExcellent();
        }
    }
}