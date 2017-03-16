using CrafterExperiment.Buffs;

namespace CrafterExperiment.Abilities
{
    public class Innovation:Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseCraftPoints(18);
            craftActions.ApplyBuff(new InnovationBuff());
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 18;
        }
    }
}