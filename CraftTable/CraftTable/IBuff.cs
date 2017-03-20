using CraftTable.Contracts;

namespace CraftTable
{
    public interface IBuff
    {
        bool IsActive { get; }
        void Step(IBuffActionsRegistry buffActionsRegistry);
        void Kill();
        void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder);
    }
}