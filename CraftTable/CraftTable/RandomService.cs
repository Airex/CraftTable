using System;
using System.Linq;
using CraftTable.Contracts;

namespace CraftTable
{
    public class RandomService:IRandomService
    {
        readonly Random _random = new Random();

        public int SelectItem(double[] chances)
        {
            var count = chances.Count(double.IsPositiveInfinity);
            if (count == 0)
                throw new ArgumentException("One item with PositiveInfinity required", nameof(chances));
            if (count > 1)
                throw new ArgumentException("More then 1 chance with PositiveInfinity is not allowed", nameof(chances));
            var nextDouble = _random.NextDouble();

            var enumerable = chances.Select((d, i) => new {Index = i, Chance = d}).ToArray();
            var orderedEnumerable = enumerable.OrderBy(arg => arg.Chance).ToArray();
            var choise = orderedEnumerable.FirstOrDefault(arg => arg.Chance>0 && arg.Chance / 100 >= nextDouble);

            choise = choise ?? enumerable.Single(arg => double.IsPositiveInfinity(arg.Chance));
            return choise.Index;
        }
    }
}