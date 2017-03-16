using Autofac;
using CraftTable.Abilities;
using CraftTable.Abilities.Specialist;
using CraftTable.Contracts;
using NUnit.Framework;

namespace CraftTable.Tests
{
    [TestFixture]
    public class CraftTableTests
    {
        [Test]
        public void BasicTouchTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();


            craftTable.Act(new BasicTouch());

            craftTable.AssertStats(1000 - 10, 0, 317, 2, 10000 - 18);
        }

        

        [Test]
        public void GreatStrydesTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new GreatStrides());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());

            craftTable.AssertStats(1000 - 40, 0, 635 + 317 * 3, 6, 10000 - 32 - 4 * 18);
        }

      

        [Test]
        public void InnerQuiteTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new InnerQuite());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());


            craftTable.AssertStats(1000 - 50, 0, 2222, 7, 10000 - 18 - 5 * 18);
        }

        [Test]
        public void TouchesTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new BasicTouch());
            craftTable.Act(new StandartTouch());
            craftTable.Act(new AdvancedTouch());


            craftTable.AssertStats(1000 - 30, 0, 1190, 4, 10000 - 18 - 32 - 48);
        }

        [Test]
        public void InnovationTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new Innovation());
            craftTable.Act(new BasicTouch());

            craftTable.AssertStats(1000 - 10, 0, 475, 3, 10000 - 18 - 18);
        }

        [Test]
        public void InnovationWinGreatStridesTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new Innovation());
            craftTable.Act(new GreatStrides());
            craftTable.Act(new BasicTouch());



            craftTable.AssertStats(1000 - 10, 0, 950, 4, 10000 - 18 - 18 - 32);
        }

        [Test]
        public void ComfortZoneTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new ComfortZone());
            for (int i = 0; i < 10; i++)
            {
                craftTable.Act(new BasicTouch());
            }
            craftTable.AssertStats(1000 - 100, 0, 317 * 10, 12, 10000 - 66 + 80 - 10 * 18);
        }


        [Test]
        public void ByregotsBlessingTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new InnerQuite());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new ByregotsBlessing());
            craftTable.AssertStats(1000 - 30, 0, 1316, 5, 10000 - 18 * 3 - 24);
        }

        [Test]
        public void ByregotsMiracleTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new InnerQuite());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new ByregotsMiracle());
            craftTable.AssertStats(1000 - 50, 0, 2451, 7, 10000 - 18 * 5 - 16);
        }


        [Test]
        public void MuscleMemoryTest()
        {
            var craftTable = TestData.CreateFactory()(new Recipe(478,60,3000), TestData.DefatltCraftMAn);

            craftTable.Act(new MuscleMemory());

            craftTable.AssertStats(50, 157, 0, 2, 10000 - 6);
        }

        [Test]
        public void WasteNotTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new WasteNot());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.AssertStats(1000 - 15, 0, 317 * 3, 5, 10000 - 18 * 3 - 56);
        }

        [Test]
        public void CarefulSunthesisTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new CarefulSynthesis2());

            craftTable.AssertStats(1000 - 10, 222, 0, 2, 10000);
        }

        [Test]
        public void CarefulSunthesis2Test()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new CarefulSynthesis2());

            craftTable.AssertStats(1000 - 10, 222, 0, 2, 10000);
        }

        [Test]
        public void PreciseTouchTest()
        {
            var craftTable = TestData.CreateFactory(registry =>
            {
                registry.WithConditions(Condition.Good,Condition.Normal);
            }).WithDefaults();

            Assert.That(craftTable, Is.Not.Null);

            craftTable.Act(new InnerQuite());
            craftTable.Act(new PreciseTouch());
            craftTable.Act(new BasicTouch());

            craftTable.AssertStats(1000 - 20, 0, 919, 4, 10000 - 18 * 3);
        }

        [Test]
        public void FailedAbilityTest()
        {
            var craftTable = TestData.CreateFactory(registry =>
            {
                registry.Builder.Register(context => new StaticRandomService(1)).As<IRandomService>();
            }).WithDefaults();

            Assert.That(craftTable, Is.Not.Null);
            
            craftTable.Act(new BasicTouch());

            craftTable.AssertStats(1000 - 10, 0, 0, 2, 10000 - 18 );
        }

       
    }
}