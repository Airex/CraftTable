using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    public class ReclaimBuff:IBuff
    {
        public bool IsActive { get; } = true;
        public void Step(IBuffActions buffActions)
        {
            
        }

        public void Kill()
        {
            
        }

        public void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder)
        {
            
        }
    }
}