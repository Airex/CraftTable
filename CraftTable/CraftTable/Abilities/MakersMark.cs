using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    public class MakersMark:Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseCraftPoints(20);
            craftActions.ApplyBuff(new MakersMarkBuff(2)); //todo: calculate steps
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 20 && serviceState.StepNumber == 2;
        }
    }
}