namespace CraftTable.Contracts
{
    public interface ICalculator
    {
        int CalculateDurability(int value);
        int CalculateProgress(int efficiency, int value, int recipeLevel, int craftmanLevel);
        int CalculateQuality(int efficiency, int value, int recipeLevel, int craftmanLevel);
        int CalculateCraftPoints(int value);

        ICalculatorBuilder GetBuilder();

        void Reset(Condition condition);

        int CalculateChance(int abilityChance);
        double CalculateConditionChance(Condition condition, int value);
        void Fail();
        double CalculateReclaimChance(int reclaimChance);
    }
}