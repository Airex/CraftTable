using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    public class PieceByPiece:Ability
    {
        public override void Execute(ICraftActions craftActions)
        {
            craftActions.UseDurability(10);
            craftActions.UseCraftPoints(15);
            craftActions.Synth(Synth.FrompPercent(33));
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return serviceState.CraftPointsLeft >= 15;
        }

        public override int Chance { get; } = 90;
    }
}