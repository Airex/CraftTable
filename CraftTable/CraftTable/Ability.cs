namespace CrafterExperiment
{
    public abstract class Ability
    {
        public abstract void Execute(ICraftActions craftActions);
        public virtual int Chance { get; } = 100;
        public abstract bool CanAct(ICraftServiceState serviceState);
    }
}