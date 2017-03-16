using CraftTable.Contracts;

namespace CraftTable
{
    public class CraftServiceState : ICraftServiceState
    {
        public CraftServiceState(Condition condition, int craftPointsLeft, int stepNumber, IBuffAccessor buffAccessor)
        {
            Condition = condition;
            CraftPointsLeft = craftPointsLeft;
            StepNumber = stepNumber;
            BuffAccessor = buffAccessor;
        }

        public int StepNumber { get; }
        public Condition Condition { get; }
        public int CraftPointsLeft { get; }
        public IBuffAccessor BuffAccessor { get; }
    }
}