using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.Culinarian, 100109)]
    [AbilityXivDb(Crafter.Alchemist, 100093)]
    [AbilityXivDb(Crafter.GoldSmith, 100070)]
    [AbilityXivDb(Crafter.Weaver, 100064)]
    [AbilityXivDb(Crafter.Leatherworker, 100048)]
    [AbilityXivDb(Crafter.Armorer, 100034)]
    [AbilityXivDb(Crafter.BlackSmith, 100018)]
    [AbilityXivDb(Crafter.Carpenter, 100004)]
    public class StandartTouch : Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseCraftPoints(32);
            craftActions.UseDurability(10);
            craftActions.Touch(125);
        }

        public override int Chance { get; } = 80;

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 32;
        }
    }
}