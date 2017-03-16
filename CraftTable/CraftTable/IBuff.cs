using CraftTable.Contracts;

namespace CraftTable
{
    public interface IBuff
    {
        bool IsActive { get; }
        void Step(IBuffActions buffActions);
        void Kill();
        void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder);
    }
}