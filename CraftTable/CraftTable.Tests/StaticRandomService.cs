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

        public int Select(double[] chances)
        {
            var step = _step++;
            return step < _values.Length ? _values[step] : 0;
        }
    }
   
}