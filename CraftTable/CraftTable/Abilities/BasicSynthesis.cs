using CraftTable.Attributes;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.Culinarian, 100105)]
    [AbilityXivDb(Crafter.Alchemist, 100090)]
    [AbilityXivDb(Crafter.GoldSmith, 100075)]
    [AbilityXivDb(Crafter.Weaver, 100060)]
    [AbilityXivDb(Crafter.Leatherworker, 100045)]
    [AbilityXivDb(Crafter.Armorer, 100030)]
    [AbilityXivDb(Crafter.BlackSmith, 100015)]
    [AbilityXivDb(Crafter.Carpenter, 100001)]
    [AbilityDescriptor("Basic synthesis", Crafter.All, 0, false, Category.Synhtesis, 0)]
    public class BasicSynthesis : Ability
    {
        public override int Chance { get; } = 90;

        public override void Execute(ICraftActions craftActions, bool isSuccess)
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