using CraftTable.Attributes;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.Culinarian, 100111)]
    [AbilityXivDb(Crafter.Alchemist, 100096)]
    [AbilityXivDb(Crafter.GoldSmith, 100080)]
    [AbilityXivDb(Crafter.Weaver, 100067)]
    [AbilityXivDb(Crafter.Leatherworker, 100051)]
    [AbilityXivDb(Crafter.Armorer, 100037)]
    [AbilityXivDb(Crafter.BlackSmith, 100021)]
    [AbilityXivDb(Crafter.Carpenter, 100007)]
    public class StandartSynthesis : Ability
    {
        public override int Chance { get; } = 90;

        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseDurability(10);
            craftActions.UseCraftPoints(15);
            craftActions.Synth(Synth.FromEfficiency(150));
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 15;
        }
    }
}