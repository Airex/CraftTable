namespace CrafterExperiment
{
    public interface IBuff
    {
        bool IsActive { get; }
        void Step(IBuffActions buffActions);
        void Kill();
        void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder);
    }
}