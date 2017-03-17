using System;
using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    public class Rumination:Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            var craftPoints = craftActions.CalculateDependency((a, b) =>
            {
                var stacks = a.GetBuff<InnerQuietBuff>().Stacks;
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