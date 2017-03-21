using System.Linq;
using System.Text;
using CraftTable.Attributes;
using CraftTable.Contracts;

namespace CraftTable
{
    public static class Extensions
    {
        public static bool IsGoodOrExcellent(this Condition condition)
        {
            return condition == Condition.Good || condition == Condition.Excellent;
        }

//        public static void Fail(this ICalculator calculator)
//        {
//            calculator.GetBuilder().ForQuality((efficincy, control, c) => { efficincy.Multiply(0); });
//            calculator.GetBuilder().ForProgress((efficincy, crafmanship, c) => { efficincy.Multiply(0); });
//        }

        public static string Name(this Ability ability)
        {
            var builder = new StringBuilder();
            foreach (var c in ability.GetType().Name)
            {
                if ((char.IsUpper(c) || char.IsDigit(c)) && builder.Length>0) builder.Append(" ");
                builder.Append(c);
            }
            return builder.ToString();
        }

        public static AbilityDescriptorAttribute AbilityDescriptor(this Ability ability)
        {
            var descriptor = ability.GetType()
                .GetCustomAttributes(typeof(AbilityDescriptorAttribute), false).Cast<AbilityDescriptorAttribute>()
                .SingleOrDefault();
            return descriptor;
        }



        public static string IdForCrafter(this Ability ability, Crafter crafter)
        {
            var descriptor = ability.GetType()
                .GetCustomAttributes(typeof(AbilityXivDbAttribute),false).Cast<AbilityXivDbAttribute>()
                .SingleOrDefault(attribute => attribute.CrafterLink == crafter || attribute.CrafterLink == Crafter.All);
            return descriptor?.AbilityId.ToString();
        }

        public static string Id(this IBuff ability)
        {
            var descriptor = ability.GetType()
                .GetCustomAttributes(typeof(BuffXivDbAttribute), false).Cast<BuffXivDbAttribute>()
                .SingleOrDefault();
            return descriptor?.BuffId.ToString();
        }
    }
}