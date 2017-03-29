using System;
using CraftTable.Contracts;

namespace CraftTable.CraftActors
{
    public class CalculatorActor : ICalculatorActor
    {
        private double _value;

        public CalculatorActor(double value)
        {
            _value = value;
        }

        public void Add(double value)
        {
            _value += value;
        }

        public void AddPercent(double value)
        {
            _value = (int)(_value * (1.0 + value / 100));
        }

        public void Multiply(double value)
        {
            _value = _value * value;
        }

        public double Value => _value;
    }
}