using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    public class ComfortZoneBuff : StepsBasedBuff
    {
        public ComfortZoneBuff() : base(10)
        {
        }

        protected override void OnStep(IBuffActions buffActions)
        {
            buffActions.RestoreCraftPoints(8);
        }

        public override void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder)
        {

        }
    }
}