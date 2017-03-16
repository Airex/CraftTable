using CrafterExperiment.Buffs;

namespace CrafterExperiment.Abilities
{
    public class SteadyHand2Ability : Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseCraftPoints(25);
            craftActions.ApplyBuff(new SteadyHandBuff(30));
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 25;
        }
    }
}