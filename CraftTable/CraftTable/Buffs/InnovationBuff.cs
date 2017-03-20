using CraftTable.Attributes;
using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    [BuffXivDb(259)]
    public class InnovationBuff:StepsBasedBuff
    {
        public InnovationBuff() : base(3)
        {
        }

        public override void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder)
        {
            calculatorBuilder.ForQuality((efficincy, control,s) =>
            {
                control.AddPercent(50);
            });
        }
    }
}