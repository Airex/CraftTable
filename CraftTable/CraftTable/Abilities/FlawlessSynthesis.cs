using System.Runtime.Remoting.Messaging;
using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    public class FlawlessSynthesis : Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseCraftPoints(15);
            craftActions.UseDurability(10);
            craftActions.Synth(Synth.FromRawValue(40));
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 0 || serviceState.BuffAccessor.GetBuff<MakersMarkBuff>() != null;
        }
    }
}