namespace CrafterExperiment.Abilities
{
    public class TricksOTheTrade:Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.RestoreCraftPoints(20);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.Condition == Condition.Extreme || serviceState.Condition == Condition.Good;
        }
    }
}