using CraftTable.Contracts;

namespace CraftTable
{
    public class ConditionService : IConditionService
    {
        private readonly IRandomService _randomService;
        private readonly ICalculator _calculator;

        public ConditionService(IRandomService randomService, ICalculator calculator)
        {
            _randomService = randomService;
            _calculator = calculator;
        }

        public Condition GetCondition(Condition? condition)
        {
            Condition result;

            if (!condition.HasValue)
                result = Condition.Normal;
            else if (condition.GetValueOrDefault() == Condition.Excellent)
                result = Condition.Poor;
            else if (condition.GetValueOrDefault() == Condition.Good)
                result = Condition.Normal;
            else if (condition.GetValueOrDefault() == Condition.Poor)
                result = Condition.Normal;
            else
            {
                var chances = new[]
                {
                    1000,
                    _calculator.CalculateConditionChance(Condition.Good, 23),
                    _calculator.CalculateConditionChance(Condition.Excellent, 1)
                };
                result = (Condition) _randomService.Select(chances);
            }
            return result;
        }
    }
}