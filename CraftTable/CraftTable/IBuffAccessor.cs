namespace CrafterExperiment
{
    public interface IBuffAccessor
    {
        T GetBuff<T>() where T : IBuff;
    }
}