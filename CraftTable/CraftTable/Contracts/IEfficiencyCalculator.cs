namespace CraftTable.Contracts
{
    public interface IEfficiencyCalculator
    {
        double CraftmanshipToProgress(double craftmanship, double efficiency, int levelDifference);
        double ControlToProgress(double control, double efficiency, int levelDifference);

        void UseConditionMultylier(double m);
    }
}