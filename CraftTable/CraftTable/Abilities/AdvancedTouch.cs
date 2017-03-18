using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.Culinarian, 100112)]
    [AbilityXivDb(Crafter.Alchemist, 100097)]
    [AbilityXivDb(Crafter.GoldSmith, 100081)]
    [AbilityXivDb(Crafter.Weaver, 100068)]
    [AbilityXivDb(Crafter.Leatherworker, 100052)]
    [AbilityXivDb(Crafter.Armorer, 100038)]
    [AbilityXivDb(Crafter.BlackSmith, 100022)]
    [AbilityXivDb(Crafter.Carpenter, 100008)]
    public class AdvancedTouch : Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseCraftPoints(48);
            craftActions.UseDurability(10);
            craftActions.Touch(150);
        }

        public override int Chance { get; } = 90;

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 48;
        }
    }
}