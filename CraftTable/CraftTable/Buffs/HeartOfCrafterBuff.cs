using CraftTable.Attributes;
using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    [BuffXivDb(879)]
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
    }
}