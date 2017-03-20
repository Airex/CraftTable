using CraftTable.Attributes;
using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities.Specialist
{
    [AbilityXivDb(Crafter.All,100152 )]
    public class ByregotsMiracle : Ability
    {
        public override int Chance { get; } = 70;

        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseCraftPoints(16);
            craftActions.UseDurability(10);
            craftActions.Touch(100);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 16 && serviceState.BuffAccessor.GetBuff<InnerQuietBuff>()?.Stacks > 1;
        }
    }
}