namespace CrafterExperiment.Abilities
{
    public class CarefulSynthesis : Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseDurability(10);
            craftActions.Synth(90);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return true;
        }
    }

    public class CarefulSynthesis2 : Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseDurability(10);
            craftActions.Synth(120);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return true;
        }
    }


    public class BasicSynthesis : Ability
    {
        public override int Chance { get; } = 90;

        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseDurability(10);
            craftActions.Synth(100);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return true;
        }
    }

    public class StandartSynthesis : Ability
    {
        public override int Chance { get; } = 90;

        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseDurability(10);
            craftActions.UseCraftPoints(15);
            craftActions.Synth(150);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 15;
        }
    }

    public class MuscleMemory : Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseCraftPoints(6);
            craftActions.UseDurability(10);
            craftActions.SynthPercent(33);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 6 && serviceState.StepNumber == 2;
        }
    }
}