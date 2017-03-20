using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    public abstract class StepsBasedBuff : IBuff, ISteps
    {
        private int _steps;

        protected StepsBasedBuff(int steps)
        {
            _steps = steps;
        }

        public bool IsActive => _steps > 0;

        public void Step(IBuffActionsRegistry buffActionsRegistry)
        {
            _steps--;
            OnStep(buffActionsRegistry);
        }

        public void Kill()
        {
            _steps = 0;
        }

        public abstract void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder);

        protected virtual void OnStep(IBuffActionsRegistry buffActions)
        {
            
        }

        public int Steps => _steps;
    }
}