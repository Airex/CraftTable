namespace CrafterExperiment.Buffs
{
    public class WasteNotBuff:StepsBasedBuff
    {
        public WasteNotBuff(int steps) : base(steps)
        {
        }

        public override void OnCalculate(ActionInfo info, ICalculatorBuilder calculatorBuilder)
        {
            calculatorBuilder.ForDurability(durability =>
            {
                durability.AddPercent(-50);
            });
        }

        protected override void OnStep(IBuffActions buffActions)
        {
            
        }
    }
}