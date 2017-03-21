using CraftTable.Attributes;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 100039)]
    [AbilityDescriptor("Piece by piece", Crafter.Armorer, 15, true, Category.Synhtesis, 5)]
    public class PieceByPiece:Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
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