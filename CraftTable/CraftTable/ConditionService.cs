using System.Data.Odbc;
using Autofac;
using CraftTable.Contracts;

namespace CraftTable
{
    public class ConditionService : IConditionService
    {
        private readonly IRandomService _randomService;
        private Condition? _prevCondition;

        public ConditionService(IRandomService randomService)
        {
            _randomService = randomService;
        }

        public Condition GetCondition(ICalculator calculator)
        {
            Condition result;

            if (!_prevCondition.HasValue)
                result = Condition.Normal;
            else if (_prevCondition.GetValueOrDefault() == Condition.Extreme)
                result = Condition.Poor;
            else if (_prevCondition.GetValueOrDefault() == Condition.Good)
                result = Condition.Normal;
            else if (_prevCondition.GetValueOrDefault() == Condition.Poor)
                result = Condition.Normal;
            else
            {
                var chances = new[]
                {
                    double.PositiveInfinity,
                    calculator.CalculateConditionChance(Condition.Good, 23),
                    calculator.CalculateConditionChance(Condition.Extreme, 1)
                };
                result = (Condition) _randomService.Select(chances);
            }
            _prevCondition = result;
            return result;
        }
    }
}