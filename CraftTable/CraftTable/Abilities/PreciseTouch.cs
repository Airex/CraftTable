using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    public class PreciseTouch:Ability
    {
        public override int Chance { get; } = 70;

        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseCraftPoints(18);
            craftActions.UseDurability(10);
            craftActions.Touch(100);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 18 && serviceState.Condition.IsGoodOrExtreme();
        }
    }
}