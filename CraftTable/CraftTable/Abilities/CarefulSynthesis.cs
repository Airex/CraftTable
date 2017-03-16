using CraftTable.Contracts;

namespace CraftTable.Abilities
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
}