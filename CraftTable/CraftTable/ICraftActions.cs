namespace CrafterExperiment
{
    public interface ICraftActions : IBuffActions
    {
        void ApplyBuff(IBuff buff);
        void Synth(int efficiency);
        void SynthPercent(int efficiency);
        void Touch(int efficiency);
        void UseCraftPoints(int craftPoints);
        void UseDurability(int durability);
    }
}