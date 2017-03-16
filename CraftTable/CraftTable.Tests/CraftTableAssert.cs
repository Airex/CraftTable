
using CrafterExperiment;
using NUnit.Framework;

namespace CrafterExperimentTests
{
    public static class CraftTableAssert
    {
        public static void AssertStats(this CraftTable craftTable, int durability, int progress, int quality, int step,
            int craftPointsLeft)
        {
            Assert.That(craftTable.Durability, Is.EqualTo(durability),"Durability");
            Assert.That(craftTable.Progress, Is.EqualTo(progress), "Progress");
            Assert.That(craftTable.Step, Is.EqualTo(step),"Step");
            Assert.That(craftTable.Quality, Is.EqualTo(quality),"Quality");
            Assert.That(craftTable.CraftPoints, Is.EqualTo(craftPointsLeft),"CraftPoints");
        }
    }
}