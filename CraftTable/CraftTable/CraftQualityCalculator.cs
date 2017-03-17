using CraftTable.Contracts;

namespace CraftTable
{
    public class CraftQualityCalculator:ICraftQualityCalculator
    {
        public int CalculateHighQualityChance(int quality, int maxQuality)
        {
            return 50;
        }
    }
}