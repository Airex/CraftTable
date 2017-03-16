namespace CraftTable
{
    public static class Extensions
    {
        public static bool IsGoodOrExtreme(this Condition condition)
        {
            return condition == Condition.Good || condition == Condition.Extreme;
        }
    }
}