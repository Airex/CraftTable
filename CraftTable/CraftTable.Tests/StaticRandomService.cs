using CraftTable.Contracts;

namespace CraftTable.Tests
{
    public class StaticRandomService:IRandomService
    {
        private readonly int _value;

        public StaticRandomService(int value)
        {
            _value = value;
        }

        public int SelectItem(int[] chances)
        {
            return _value;
        }
    }
}