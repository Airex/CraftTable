using CraftTable.Contracts;

namespace CraftTable.Abilities
{
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
}