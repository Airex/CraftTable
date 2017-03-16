using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities.Specialist
{
    public class InnovativeTouch : Ability
    {
        public override int Chance { get; } = 40;

        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseCraftPoints(8);
            craftActions.UseDurability(10);
            craftActions.Touch(100);
            craftActions.ApplyBuff(new InnovationBuff());
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 8;
        }
    }
}