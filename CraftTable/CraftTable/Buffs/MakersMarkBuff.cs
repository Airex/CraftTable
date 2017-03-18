using CraftTable.Abilities;
using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    [BuffXivDb(878)]
    public class MakersMarkBuff:StepsBasedBuff
    {
        public MakersMarkBuff(int steps) : base(steps)
        {
        }

        public override void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder)
        {
            if (info.AbilityType == typeof(FlawlessSynthesis))
            {
                calculatorBuilder.ForDurability(durability => durability.Multiply(0));
                calculatorBuilder.ForCraftPoints(points => points.Multiply(0));
            }

        }

        protected override void OnStep(IBuffActions buffActions)
        {
            
        }
    }
}