using CraftTable.Attributes;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 100108)]
    [AbilityDescriptor("Hasty touch", Crafter.Culinarian, 0, true, Category.Quality, 3)]
    public class HastyTouch:Ability
    {
        
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseDurability(10);
            craftActions.Touch(100);
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return true;
        }

        public override int Chance { get; } = 50;
    }
}