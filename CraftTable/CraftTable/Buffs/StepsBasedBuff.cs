namespace CrafterExperiment.Buffs
{
    public abstract class StepsBasedBuff : IBuff
    {
        private int _steps;

        protected StepsBasedBuff(int steps)
        {
            _steps = steps;
        }

        public bool IsActive => _steps > 0;

        public void Step(IBuffActions buffActions)
        {
            _steps--;
            OnStep(buffActions);
        }

        public void Kill()
        {
            _steps = 0;
        }

        public abstract void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder);

        protected abstract void OnStep(IBuffActions buffActions);
    }
}