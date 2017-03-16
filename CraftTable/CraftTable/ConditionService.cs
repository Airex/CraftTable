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

        public Condition GetCondition()
        {
            Condition result;
            if (_prevCondition.HasValue && _prevCondition.Value == Condition.Extreme)
                result = Condition.Poor;
            else
            {
                result = (Condition) _randomService.SelectItem(new[] {double.PositiveInfinity, 8, 1,0});
            }
            _prevCondition = result;
            return result;
        }
    }
}