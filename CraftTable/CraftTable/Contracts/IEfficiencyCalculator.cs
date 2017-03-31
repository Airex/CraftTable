namespace CraftTable.Contracts
{
    public interface IEfficiencyCalculator
    {
        double CraftmanshipToProgress(double craftmanship, double efficiency, int crafterLevel, double levelDifference);
        double ControlToQuality(double control, double efficiency, int recipeLevel, double levelDifference);

        void UseConditionMultylier(double m);
    }
}