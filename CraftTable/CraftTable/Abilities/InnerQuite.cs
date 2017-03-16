using CrafterExperiment.Buffs;

namespace CrafterExperiment.Abilities
{
    public class InnerQuite : Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseCraftPoints(18);
            craftActions.ApplyBuff(new InnerQuiteBuff());
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 18 && serviceState.BuffAccessor.GetBuff<InnerQuiteBuff>() == null;
        }
    }
}