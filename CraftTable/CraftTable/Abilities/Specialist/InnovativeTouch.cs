using CraftTable.Attributes;
using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities.Specialist
{
    [AbilityXivDb(Crafter.All, 100144)]
    [AbilityDescriptor("Innovative touch", Crafter.All, 8, false, Category.Specialist, 2)]
    public class InnovativeTouch : Ability
    {
        public override int Chance { get; } = 40;

        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseCraftPoints(8);
            craftActions.UseDurability(10);
            craftActions.Touch(100);
            if (isSuccess) craftActions.ApplyBuff(new InnovationBuff());
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 8;
        }
    }
}