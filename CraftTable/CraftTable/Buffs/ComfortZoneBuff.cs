namespace CrafterExperiment.Buffs
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