using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    public class Rumination:Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.RestoreDurability(0);
            //todo: kill buff
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.BuffAccessor.GetBuff<InnerQuiteBuff>()?.Stacks >= 2;
        }
    }
}