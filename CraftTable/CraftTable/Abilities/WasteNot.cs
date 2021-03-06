﻿using CraftTable.Attributes;
using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 279)]
    [AbilityDescriptor("Waste not", Crafter.Leatherworker, 56, true, Category.Durability, 2)]
    public class WasteNot : Ability 
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
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