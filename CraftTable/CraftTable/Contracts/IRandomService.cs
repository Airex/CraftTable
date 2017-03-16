namespace CraftTable.Contracts
{
    public interface IRandomService
    {
        int SelectItem(int[] chances);
    }
}