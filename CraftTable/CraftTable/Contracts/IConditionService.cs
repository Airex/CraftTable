namespace CraftTable.Contracts
{
    public interface IConditionService
    {
        Condition GetCondition(ICalculator calculator);
    }
}