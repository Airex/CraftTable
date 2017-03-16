﻿using CrafterExperiment.Buffs;

namespace CrafterExperiment.Abilities
{
    public class ByregotBlessing : Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseCraftPoints(24);
            craftActions.Touch(100);
            craftActions.UseDurability(10);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 24 && serviceState.BuffAccessor.GetBuff<InnerQuiteBuff>()?.Stacks > 1;
        }

        public override int Chance { get; } = 90;
    }
}