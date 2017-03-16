using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    public class InnovationBuff:StepsBasedBuff
    {
        public InnovationBuff() : base(3)
        {
        }

        public override void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder)
        {
            calculatorBuilder.ForQuality((efficincy, control) =>
            {
                control.AddPercent(50);
            });
        }

        protected override void OnStep(IBuffActions buffActions)
        {
            
        }
    }
}