namespace CraftTable.Contracts
{
    public interface IBuffAccessor
    {
        T GetBuff<T>() where T : IBuff;
    }
}