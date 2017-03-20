using CraftTable.Attributes;
using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    [BuffXivDb(260)]
    public class ReclaimBuff:IBuff
    {
        public bool IsActive { get; } = true;

        public void Step(IBuffActionsRegistry buffActionsRegistry)
        {
            
        }

        public void Kill()
        {
            
        }

        public void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder)
        {
            calculatorBuilder.ForReclaimChance(action =>
            {
                action.Multiply(0);
                action.Add(90);
            });
        }

        
    }
}