﻿using CraftTable.Attributes;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 100063)]
    [AbilityDescriptor("Careful synthesis", Crafter.Weaver, 0, true, Category.Synhtesis, 3)]
    public class CarefulSynthesis : Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseDurability(10);
            craftActions.Synth(Synth.FromEfficiency(90));
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return true;
        }
    }
}