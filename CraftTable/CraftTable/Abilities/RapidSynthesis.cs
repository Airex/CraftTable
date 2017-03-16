using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    public class RapidSynthesis:Ability
    {
        public override int Chance { get; } = 50;

        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseDurability(10);
            craftActions.Synth(Synth.FromEfficiency(250));
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return true;
        }
    }
}