﻿using CraftTable.Attributes;
using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 100083)]
    [AbilityDescriptor("Flawless synthesis", Crafter.GoldSmith, 15, true, Category.Synhtesis, 2)]
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
            return serviceState.CraftPointsLeft >= 15 || serviceState.BuffAccessor.GetBuff<MakersMarkBuff>() != null;
        }
    }
}