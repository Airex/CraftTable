using System;
using System.Linq;
using CraftTable.Contracts;

namespace CraftTable
{
    public class RandomService : IRandomService
    {
        readonly Random _random = new Random();

        public int SelectItem(double[] chances)
        {
            var count = chances.Count(double.IsPositiveInfinity);
            if (count == 0)
                throw new ArgumentException("One item with PositiveInfinity required", nameof(chances));
            if (count > 1)
                throw new ArgumentException("More then 1 chance with PositiveInfinity is not allowed", nameof(chances));
            if (chances.Where(d => !double.IsPositiveInfinity(d)).Sum() > 100)
                throw new ArgumentException("Total amount of chances must be less or equals 100.0", nameof(chances));
            var nextDouble = _random.NextDouble();

            var enumerable = chances.Select((d, i) => new { Index = i, Chance = d }).ToArray();
            var orderedEnumerable = enumerable.OrderBy(arg => arg.Chance).ToArray();


            double low = 0;
            for (var i = 0; i < orderedEnumerable.Length - 1; i++)
            {
                var chance = orderedEnumerable[i].Chance / 100;
                if (nextDouble >= low && nextDouble <= low + chance)
                {
                    return orderedEnumerable[i].Index;
                }
                low += chance;
            }
            return enumerable.Single(arg => double.IsPositiveInfinity(arg.Chance)).Index;

        }
    }
}