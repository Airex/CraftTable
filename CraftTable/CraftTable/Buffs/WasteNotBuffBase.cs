using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    public class WasteNotBuffBase : StepsBasedBuff
    {
        public WasteNotBuffBase(int steps) : base(steps)
        {
        }

        public override void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder)
        {
            calculatorBuilder.ForDurability(durability =>
            {
                durability.AddPercent(-50);
            });
        }

        protected override void OnStep(IBuffActions buffActions)
        {
            
        }
    }
}