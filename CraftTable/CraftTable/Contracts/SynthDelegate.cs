namespace CraftTable.Contracts
{
    public delegate int SynthDelegate(Recipe r, CraftMan c, int currentProgress, ICalculator calculator);
}