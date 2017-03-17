using CraftTable.Contracts;

namespace CraftTable
{
    public delegate int SynthDelegate(Recipe r, CraftMan c, int currentProgress, ICalculator calculator);
}