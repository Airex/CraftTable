using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    public class WasteNot : Ability 
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseCraftPoints(56);
            craftActions.ApplyBuff(new WasteNotBuff(4));
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 56;
        }
    }
}