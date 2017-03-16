using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities.Specialist
{
    public class WhistleWhileYouWork : Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseCraftPoints(36);
            craftActions.ApplyBuff(new WhistleBuff());
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 36;
        }
    }
}