using CraftTable.Attributes;
using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    [BuffXivDb(260)]
    public class ReclaimBuff:IBuff
    {
        public bool IsActive { get; } = true;
        public void Step(IBuffActions buffActions)
        {
            
        }

        public void Step(IBuffActionsRegistry buffActionsRegistry)
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