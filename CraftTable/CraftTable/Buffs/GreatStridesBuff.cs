using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    public class GreatStridesBuff : StepsBasedBuff
    {
        public GreatStridesBuff() : base(3)
        {
        }

        protected override void OnStep(IBuffActions buffActions)
        {

        }

        public override void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder)
        {
            calculatorBuilder.ForQuality((efficiency, control) =>
            {
                efficiency.Multiply(2);
                Kill();
            });
        }
    }
}