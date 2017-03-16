using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    public class InguenityBuff : StepsBasedBuff
    {
        private readonly int _power;
        public InguenityBuff(int power) : base(5)
        {
            _power = power;
        }

        public override void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder)
        {
            calculatorBuilder.ForRecipeLevel(level =>
            {
                level.Add(-_power);
            });
        }

        protected override void OnStep(IBuffActions buffActions)
        {
            
        }
    }
}