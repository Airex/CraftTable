using CraftTable.Contracts;

namespace CraftTable
{
    public abstract class Ability
    {
        public abstract void Execute(ICraftActions craftActions, bool isSuccess);
        public virtual int Chance { get; } = 100;
        public abstract bool CanAct(ICraftServiceState serviceState);

        public override string ToString()
        {
            return this.Name();
        }
    }
}