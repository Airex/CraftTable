using NUnit.Framework;

namespace CraftTable.Tests.Infrastructure
{
    public static class CraftTableAssert
    {
        public static void AssertStats(this CraftTable craftTable, int durability, int progress, int quality, int step,
            int craftPointsLeft)
        {
            var craftTableInfo = craftTable.GetStatus();
            Assert.That(craftTableInfo.Durability, Is.EqualTo(durability),"Durability");
            Assert.That(craftTableInfo.Progress, Is.EqualTo(progress), "Progress");
            Assert.That(craftTableInfo.Step, Is.EqualTo(step),"Step");
            Assert.That(craftTableInfo.Quality, Is.EqualTo(quality),"Quality");
            Assert.That(craftTableInfo.CraftPoints, Is.EqualTo(craftPointsLeft),"CraftPoints");
        }
    }
}