namespace CraftTable.Contracts
{
    public interface ICalculatorActor
    {
        void Add(double value);
        void AddPercent(double value);
        void Multiply(double value);
    }
}