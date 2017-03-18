using System;
using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 100178)]
    public class MakersMark:Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseCraftPoints(20);
            var makersMarkStacks = (int)Math.Round((double)craftActions.CalculateDependency((a,b,c)=>c.Difficulty) / 100);
            if (makersMarkStacks == 0)
            {
                makersMarkStacks = 1;
            }
            craftActions.ApplyBuff(new MakersMarkBuff(makersMarkStacks)); 
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 20 && serviceState.StepNumber == 1;
        }
    }
}