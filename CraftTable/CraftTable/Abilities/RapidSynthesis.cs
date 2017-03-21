using CraftTable.Attributes;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 100033)]
    [AbilityDescriptor("Rapid synthesis", Crafter.Armorer, 0, true, Category.Synhtesis, 6)]
    public class RapidSynthesis:Ability
    {
        public override int Chance { get; } = 50;

        public override void Execute(ICraftActions craftActions, bool isSuccess)
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