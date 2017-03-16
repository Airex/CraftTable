using CraftTable.Contracts;

namespace CraftTable
{
    public static class Extensions
    {
        public static bool IsGoodOrExtreme(this Condition condition)
        {
            return condition == Condition.Good || condition == Condition.Extreme;
        }

        public static void Fail(this ICalculator calculator)
        {
            calculator.GetBuilder().ForQuality((efficincy, control) => { efficincy.Multiply(0); });
            calculator.GetBuilder().ForProgress((efficincy, crafmanship) => { efficincy.Multiply(0); });
        }
    }
}