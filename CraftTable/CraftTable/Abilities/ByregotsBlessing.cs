﻿using CraftTable.Attributes;
using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 100009)]
    [AbilityDescriptor("Byregot's blessing", Crafter.Carpenter, 24, true, Category.Quality, 4)]
    public class ByregotsBlessing : Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseCraftPoints(24);
            craftActions.Touch(100);
            craftActions.UseDurability(10);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 24 && serviceState.BuffAccessor.GetBuff<InnerQuietBuff>()?.Stacks > 1;
        }

        public override int Chance { get; } = 90;
    }
}