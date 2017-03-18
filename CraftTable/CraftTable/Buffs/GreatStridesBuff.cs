using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    [BuffXivDb(254)]
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
            calculatorBuilder.ForQuality((efficiency, control, s) =>
            {
                efficiency.Multiply(2);
                Kill();
            });
        }
    }
}