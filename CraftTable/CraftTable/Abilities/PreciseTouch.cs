using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.Culinarian, 100135)]
    [AbilityXivDb(Crafter.Alchemist, 100134)]
    [AbilityXivDb(Crafter.GoldSmith, 100131)]
    [AbilityXivDb(Crafter.Weaver, 100133)]
    [AbilityXivDb(Crafter.Leatherworker, 100032)]
    [AbilityXivDb(Crafter.Armorer, 100030)]
    [AbilityXivDb(Crafter.BlackSmith, 100129)]
    [AbilityXivDb(Crafter.Carpenter, 100128)]
    public class PreciseTouch:Ability
    {
        public override int Chance { get; } = 70;

        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseCraftPoints(18);
            craftActions.UseDurability(10);
            craftActions.Touch(100);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 18 && serviceState.Condition.IsGoodOrExcellent();
        }
    }
}