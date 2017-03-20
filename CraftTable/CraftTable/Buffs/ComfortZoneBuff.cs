using CraftTable.Attributes;
using CraftTable.Contracts;

namespace CraftTable.Buffs
{
    [BuffXivDb(261)]
    public class ComfortZoneBuff : StepsBasedBuff
    {
        public ComfortZoneBuff() : base(10)
        {
        }

        protected override void OnStep(IBuffActionsRegistry buffActions)
        {
            buffActions.RegisterPreAbility(actions =>
            {
                actions.RestoreCraftPoints(8);
            });
            
        }

        public override void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder)
        {

        }
    }
}