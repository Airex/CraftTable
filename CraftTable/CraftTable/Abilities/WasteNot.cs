using CrafterExperiment.Buffs;

namespace CrafterExperiment.Abilities
{
    public class WasteNot : Ability 
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseCraftPoints(56);
            craftActions.ApplyBuff(new WasteNotBuff(4));
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 56;
        }
    }

    public class WasteNot2 : Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseCraftPoints(98);
            craftActions.ApplyBuff(new WasteNotBuff(8));
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 98;
        }
    }
}