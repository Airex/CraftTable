using System;
using CraftTable.Attributes;
using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 276 )]
    [AbilityDescriptor("Rumination", Crafter.Carpenter, 0, true, Category.CP, 1)]
    public class Rumination:Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            var craftPoints = craftActions.CalculateDependency((a, b,c) =>
            {
                var stacks = a.GetBuff<InnerQuietBuff>().Stacks - 1;
                a.GetBuff<InnerQuietBuff>().Kill();
                return (21 * stacks - Math.Pow(stacks, 2) + 10) / 2;
            });
            craftActions.RestoreCraftPoints((int)craftPoints);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.BuffAccessor.GetBuff<InnerQuietBuff>()?.Stacks >= 2;
        }
    }
}