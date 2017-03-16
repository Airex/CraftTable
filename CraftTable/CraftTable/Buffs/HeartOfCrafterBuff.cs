using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    public class HeartOfCrafterBuff:StepsBasedBuff
    {
        public HeartOfCrafterBuff() : base(7)
        {
        }

        public override void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder)
        {
            calculatorBuilder.ForConditionChance((condition, chance) =>
            {
                if (condition == Condition.Good)
                    chance.Add(0); // todo: get correct value for increase
            });
        }

        protected override void OnStep(IBuffActions buffActions)
        {
            
        }
    }
}