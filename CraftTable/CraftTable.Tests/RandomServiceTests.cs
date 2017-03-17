using System;
using NUnit.Framework;

namespace CraftTable.Tests
{
    public class RandomServiceTests
    {
        [Test]
        public void NoPositoveInfinityTest()
        {
            RandomService service = new RandomService();
            Assert.Throws<ArgumentException>(() => service.Select(new[] { 1.0, 1.0 }));
        }

        [Test]
        public void MoreThenOnePositoveInfinityTest()
        {
            RandomService service = new RandomService();
            Assert.Throws<ArgumentException>(() => service.Select(new[] { double.PositiveInfinity, double.PositiveInfinity }));
        }

        [Test]
        public void TotalAmountOfChancesGreater100Test()
        {
            RandomService service = new RandomService();
            Assert.Throws<ArgumentException>(() => service.Select(new[] { 60,40,10, double.PositiveInfinity }));
        }
    }
}