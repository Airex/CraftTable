using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    public class AdvancedTouch : Ability
    {
        public override void Execute(ICraftActions craftActions)
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