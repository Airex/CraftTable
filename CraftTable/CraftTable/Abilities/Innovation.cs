using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 284)]
    public class Innovation:Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseCraftPoints(18);
            craftActions.ApplyBuff(new InnovationBuff());
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 18;
        }
    }
}