using System.Runtime.Remoting.Messaging;
using System.Text;
using CraftTable.Contracts;

namespace CraftTable
{
    public static class Extensions
    {
        public static bool IsGoodOrExcellent(this Condition condition)
        {
            return condition == Condition.Good || condition == Condition.Excellent;
        }

        public static void Fail(this ICalculator calculator)
        {
            calculator.GetBuilder().ForQuality((efficincy, control) => { efficincy.Multiply(0); });
            calculator.GetBuilder().ForProgress((efficincy, crafmanship) => { efficincy.Multiply(0); });
        }

        public static string Name(this Ability ability)
        {
            var builder = new StringBuilder();
            foreach (var c in ability.GetType().Name)
            {
                if (char.IsUpper(c) && builder.Length>0) builder.Append(" ");
                builder.Append(c);
            }
            return builder.ToString();
        }
    }
}