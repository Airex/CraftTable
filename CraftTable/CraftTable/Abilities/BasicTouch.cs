using CraftTable.Attributes;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.Culinarian, 100106)]
    [AbilityXivDb(Crafter.Alchemist, 100091)]
    [AbilityXivDb(Crafter.GoldSmith, 100076)]
    [AbilityXivDb(Crafter.Weaver, 100061)]
    [AbilityXivDb(Crafter.Leatherworker, 100046)]
    [AbilityXivDb(Crafter.Armorer, 100031)]
    [AbilityXivDb(Crafter.BlackSmith, 100016)]
    [AbilityXivDb(Crafter.Carpenter, 100002)]
    [AbilityDescriptor("Basic touch", Crafter.All, 18, false, Category.Quality, 0)]
    public class BasicTouch : Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseCraftPoints(18);
            craftActions.UseDurability(10);
            craftActions.Touch(100);
        }

        public override int Chance { get; } = 70;

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 18;
        }
    }
}