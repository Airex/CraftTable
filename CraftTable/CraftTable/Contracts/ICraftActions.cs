
namespace CraftTable.Contracts
{
    public interface ICraftActions : IBuffActions
    {
        void ApplyBuff(IBuff buff);
        void Synth(SynthDelegate synth);
        void Touch(int efficiency);
        void UseCraftPoints(int craftPoints);
        void UseDurability(int durability);
        T CalculateDependency<T>(CalculateDependency<T> input) where T:struct ;
    }
}