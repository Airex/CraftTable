using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    public class MuscleMemory : Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseCraftPoints(6);
            craftActions.UseDurability(10);
            craftActions.Synth(Synth.FrompPercent(33));
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 6 && serviceState.StepNumber == 2;
        }
    }
}