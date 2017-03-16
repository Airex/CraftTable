using System;
using CraftTable.Contracts;

namespace CraftTable.Tests
{
    public class StaticRandomService:IRandomService
    {
        private int _step;
        private readonly int[] _values;

        public StaticRandomService(params int[] value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            _values = value;
        }

        public int SelectItem(double[] chances)
        {
            return _values[Math.Min(_values.Length-1,_step++)];
        }
    }
   
}