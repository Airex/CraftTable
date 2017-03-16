using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    public class StandartSynthesis : Ability
    {
        public override int Chance { get; } = 90;

        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseDurability(10);
            craftActions.UseCraftPoints(15);
            craftActions.Synth(Synth.FromEfficiency(150));
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 15;
        }
    }
}