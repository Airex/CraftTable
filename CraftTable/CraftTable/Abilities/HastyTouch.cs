using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    public class HastyTouch:Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseDurability(10);
            craftActions.Touch(100);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return true;
        }

        public override int Chance { get; } = 50;
    }
}