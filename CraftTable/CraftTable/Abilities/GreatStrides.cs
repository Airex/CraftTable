using CrafterExperiment.Buffs;

namespace CrafterExperiment.Abilities
{
    public class GreatStrides : Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseCraftPoints(32);
            craftActions.ApplyBuff(new GreatStridesBuff());
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 32;
        }
    }
}