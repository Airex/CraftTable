namespace CrafterExperiment.Abilities
{
    public class StandartTouch : Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseCraftPoints(32);
            craftActions.UseDurability(10);
            craftActions.Touch(125);
        }

        public override int Chance { get; } = 80;

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 32;
        }
    }
}