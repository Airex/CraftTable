using CraftTable.Attributes;
using CraftTable.Contracts;

namespace CraftTable.Abilities
{
    [AbilityXivDb(Crafter.All, 100069)]
    [AbilityDescriptor("Careful synthesis II", Crafter.Weaver, 16, true, Category.Synhtesis, 4)]
    public class CarefulSynthesis2 : Ability
    {
        public override void Execute(ICraftActions craftActions, bool isSuccess)
        {
            craftActions.UseDurability(10);
            craftActions.Synth(Synth.FromEfficiency(120));
        }

        public override bool CanAct(ICraftServiceState serviceState)
        {
            return true;
        }
    }
}