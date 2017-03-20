using CraftTable.Attributes;
using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    [BuffXivDb(258)]
    public class ManipulationBuff:StepsBasedBuff
    {
        public ManipulationBuff() : base(3)
        {
        }

        protected override void OnStep(IBuffActionsRegistry buffActions)
        {
            buffActions.RegisterPostAbility(actions =>
            {
                actions.RestoreDurability(10);   
            });
        }

        public override void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder)
        {

        }
    }
}