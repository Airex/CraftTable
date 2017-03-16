using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    public class BasicSynthesis : Ability
    {
        public override int Chance { get; } = 90;

        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseDurability(10);
            craftActions.Synth(Synth.FromEfficiency(100));
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return true;
        }
    }
}