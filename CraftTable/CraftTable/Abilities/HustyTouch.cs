namespace CrafterExperiment.Abilities
{
    public class HustyTouch:Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.Touch(100);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return true;
        }

        public override int Chance { get; } = 50;
    }
}