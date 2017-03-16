using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    public class ManipulationBuff:StepsBasedBuff
    {
        public ManipulationBuff() : base(3)
        {
        }

        public override void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder)
        {
            
        }

        protected override void OnStep(IBuffActions buffActions)
        {
            buffActions.RestoreDurability(10);
        }
    }
}