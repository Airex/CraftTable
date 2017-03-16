namespace CrafterExperiment
{
    public interface IBuffCollector
    {
        void Add(IBuff buff);
        void Step(IBuffActions buffActions);
        void BuildCalculator(ActionInfo info, ICalculatorBuilder calculatorBuilder);
        IBuffAccessor GetBuffAccessor();
    }
}