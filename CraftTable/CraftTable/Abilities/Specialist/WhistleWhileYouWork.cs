﻿using CraftTable.Attributes;
using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities.Specialist
{
    [AbilityXivDb(Crafter.All, 100194)]
    [AbilityDescriptor("Whistle while you work", Crafter.All, 36, false, Category.Specialist, 0)]
    public class WhistleWhileYouWork : Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
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