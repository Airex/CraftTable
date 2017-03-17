namespace CraftTable.Contracts
{
    public interface ICraftQualityCalculator
    {
        int CalculateHighQualityChance(int quality, int maxQuality);
    }
}