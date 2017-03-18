using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    public abstract class SteadyHandBuffBase : StepsBasedBuff
    {
        private readonly int _increase;

        protected SteadyHandBuffBase(int increase) : base(5)
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