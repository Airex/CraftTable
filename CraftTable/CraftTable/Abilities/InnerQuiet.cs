using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 259)]
    public class InnerQuiet : Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseCraftPoints(18);
            craftActions.ApplyBuff(new InnerQuietBuff());
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 18 && serviceState.BuffAccessor.GetBuff<InnerQuietBuff>() == null;
        }
    }
}