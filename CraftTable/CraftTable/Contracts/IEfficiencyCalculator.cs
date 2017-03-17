namespace CraftTable.Contracts
{
    public interface IEfficiencyCalculator
    {
        double CraftmanshipToProgress(double craftmanship, double efficiency, int crafterLevel, int levelDifference);
        double ControlToProgress(double control, double efficiency, int recipeLevel, int levelDifference);

        void UseConditionMultylier(double m);
    }
}