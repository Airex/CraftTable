using CraftTable.Buffs;
using CraftTable.Contracts;

namespace CraftTable.Abilities.Specialist
{
    public class TrainedHand : Ability
    {
        public override int Chance { get; } = 90;

        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseCraftPoints(32);
            craftActions.UseDurability(10);
            craftActions.Touch(150);
            craftActions.Synth(Synth.FromEfficiency(150));
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            var innerQuiteBuff = serviceState.BuffAccessor.GetBuff<InnerQuiteBuff>();
            var whistleBuff = serviceState.BuffAccessor.GetBuff<WhistleBuff>();
            return serviceState.CraftPointsLeft >= 32 && innerQuiteBuff != null && whistleBuff != null &&
                   innerQuiteBuff.Stacks == whistleBuff.Stacks;
        }
    }
}