using System.Runtime.Remoting.Messaging;
using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 100083)]
    public class FlawlessSynthesis : Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
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