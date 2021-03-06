using CraftTable.CraftActors;

namespace CraftTable.Contracts
{
    public interface ICalculatorBuilder
    {
        void ForDurability(DurabilityActor action);
        void ForProgress(ProgressActor action);
        void ForQuality(QualityActor action);
        void ForCraftPoints(CraftPointsActor action);
        void ForChance(ChanceActor action);
        void ForConditionChance(ConditionChanceActor action);
        void ForRecipeLevel(RecipeLevelActor action);
        void ForReclaimChance(ReclaimChanceActor action);
    }
}