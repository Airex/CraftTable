using CrafterExperiment.Buffs;

namespace CrafterExperiment.Abilities
{
    public class SteadyHandAbility : Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseCraftPoints(22);
            craftActions.ApplyBuff(new SteadyHandBuff(20));
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft>=22;
        }
    }
}