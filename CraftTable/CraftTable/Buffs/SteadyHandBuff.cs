using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    public class SteadyHandBuff : StepsBasedBuff
    {
        private readonly int _increase;

        public SteadyHandBuff(int increase) : base(5)
        {
            _increase = increase;
        }

        public override void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder)
        {
            calculatorBuilder.ForChance(chance =>
            {
                chance.Add(_increase);
            });
        }

        protected override void OnStep(IBuffActions buffActions)
        {
            
        }
    }
}