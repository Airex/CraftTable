using CraftTable.Abilities;
using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    public class InnerQuiteBuff : IBuff
    {
        public int Stacks { get; private set; }
        public bool IsActive { get; private set; } = true;


        public void Step(IBuffActions buffActions)
        {

        }

        public void Kill()
        {
            IsActive = false;
        }

        public void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder)
        {
            calculatorBuilder.ForQuality((efficiency, control) =>
            {
                int stacks = Stacks;
                if (info.AbilityType == typeof(ByregotBlessing))
                {
                    Kill();
                    efficiency.AddPercent(20 * stacks);
                }
                
                control.AddPercent(20 * stacks);
                
                if (Stacks<11)
                    Stacks++;
                
            });
        }
    }
}