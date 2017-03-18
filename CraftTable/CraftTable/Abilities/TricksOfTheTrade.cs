using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All,100098 )]
    public class TricksOfTheTrade:Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.RestoreCraftPoints(20);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.Condition.IsGoodOrExcellent();
        }
    }
}