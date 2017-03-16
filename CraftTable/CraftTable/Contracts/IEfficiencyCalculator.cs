namespace CraftTable.Contracts
{
    public interface IEfficiencyCalculator
    {
        double CraftmanshipToProgress(double craftmanship, double efficiency);
        double ControlToProgress(double control, double efficiency);

        void UseConditionMultylier(double m);
    }
}