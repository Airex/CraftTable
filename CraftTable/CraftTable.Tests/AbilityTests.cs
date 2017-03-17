using Autofac;
using CraftTable.Abilities;
using CraftTable.Abilities.Specialist;
using CraftTable.Contracts;
using NUnit.Framework;

namespace CraftTable.Tests
{
    [TestFixture]
    public class AbilityTests
    {
        [Test]
        public void BasicTouchTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();
            craftTable.Act(new BasicTouch());
            craftTable.AssertStats(1000 - 10, 0, 317, 2, 10000 - 18);
        }


        [Test]
        public void HastyTouchTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();
            craftTable.Act(new HastyTouch());
            craftTable.AssertStats(1000 - 10, 0, 317, 2, 10000);
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

            craftTable.Act(new InnerQuiet());
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

            craftTable.Act(new InnerQuiet());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new ByregotsBlessing());
            craftTable.AssertStats(1000 - 30, 0, 1316, 5, 10000 - 18 * 3 - 24);
        }

        [Test]
        public void ByregotsBrowTest()
        {
            var craftTable = TestData.CreateFactory(registry =>
            {
                registry.WithConditions(Condition.Normal, Condition.Normal, Condition.Good);
            }).WithDefaults();

            craftTable.Act(new InnerQuiet());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new ByregotsBrow());
            craftTable.AssertStats(1000 - 30, 0, 1825, 5, 10000 - 18 * 3 - 18);
        }

        [Test]
        public void ByregotsMiracleTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new InnerQuiet());
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
            var craftTable = TestData.CreateFactory()(new Recipe(478, 60, 3000), TestData.DefaultCraftman);

            craftTable.Act(new MuscleMemory());

            craftTable.AssertStats(50, 157, 0, 2, 10000 - 6);
        }

        [Test]
        public void PieceByPieceTest()
        {
            var craftTable = TestData.CreateFactory()(new Recipe(478, 60, 3000), TestData.DefaultCraftman);

            craftTable.Act(new PieceByPiece());

            craftTable.AssertStats(50, 157, 0, 2, 10000 - 15);
        }

        [Test]
        public void WasteNotTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new WasteNot());
            for (int i = 0; i < 5; i++)
            {
                craftTable.Act(new BasicTouch());
            }
            
            craftTable.AssertStats(1000 - 30, 0, 317 * 5, 7, 10000 - 18 * 5 - 56);
        }

        [Test]
        public void WasteNot2Test()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new WasteNot2());
            for (int i = 0; i < 9; i++)
            {
                craftTable.Act(new BasicTouch());
            }
            craftTable.AssertStats(1000 - 50, 0, 317 * 9, 11, 10000 - 18 * 9 - 98);
        }

        [Test]
        public void CarefulSunthesisTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new CarefulSynthesis());

            craftTable.AssertStats(1000 - 10, 166, 0, 2, 10000);
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
                registry.WithConditions(Condition.Good, Condition.Normal);
            }).WithDefaults();

            Assert.That(craftTable, Is.Not.Null);

            craftTable.Act(new InnerQuiet());
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

            craftTable.AssertStats(1000 - 10, 0, 0, 2, 10000 - 18);
        }

        [Test]
        public void NymeriasWheelTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new WhistleWhileYouWork());
            craftTable.Act(new BasicSynthesis());
            craftTable.Act(new BasicSynthesis());
            craftTable.Act(new NymeriasWheel());
            craftTable.AssertStats(1000 - 10, 370, 0, 5, 10000 - 18 -36);
        }

        [Test]
        public void Inguenity1Test()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new Inguenity());
            craftTable.Act(new BasicSynthesis());
            craftTable.Act(new BasicTouch());
            craftTable.AssertStats(1000 - 20, 251, 317, 4, 10000 - 24 - 18);
        }

        [Test]
        public void Inguenity2Test()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new Inguenity2());
            craftTable.Act(new BasicSynthesis());
            craftTable.Act(new BasicTouch());
            craftTable.AssertStats(1000 - 20, 254, 317, 4, 10000 - 32 - 18);
        }

        [Test]
        public void WhistleStackMod3IncreasesProgressTest()
        {
            var craftTable = TestData.CreateFactory(registry =>
            {
                registry.WithConditions(Condition.Good,Condition.Good, Condition.Good);
            }).WithDefaults();

            craftTable.Act(new WhistleWhileYouWork());
            craftTable.Act(new Observe());
            craftTable.Act(new Observe());
            craftTable.Act(new Observe());
            craftTable.Act(new BasicSynthesis());
            craftTable.AssertStats(1000 - 10, 277, 0, 6, 10000 - 14*3 - 36);
        }

        [Test]
        public void InnovativeTouchTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new InnovativeTouch());
            craftTable.Act(new BasicTouch());
            craftTable.AssertStats(1000 - 20, 0, 792, 3, 10000 - 8-18);
        }
       

        [Test]
        public void SatisfactionTest()
        {
            var craftTable = TestData.CreateFactory(registry =>
            {
                registry.WithConditions(Condition.Good, Condition.Good);
            }).WithDefaults();

            craftTable.Act(new WhistleWhileYouWork());
            craftTable.Act(new Observe());
            craftTable.Act(new Observe());
            craftTable.Act(new Satisfaction());
            craftTable.Act(new BasicSynthesis());

            craftTable.AssertStats(1000 - 10, 185, 0, 6, 10000 - 36 - 14 * 2 + 15);
        }

        [Test]
        public void StandartSysnthesisTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();
            craftTable.Act(new StandartSynthesis());
            craftTable.AssertStats(1000 - 10, 277, 0, 2, 10000 - 15);
        }

        [Test]
        public void RapidSysnthesisTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();
            craftTable.Act(new RapidSynthesis());
            craftTable.AssertStats(1000 - 10, 462, 0, 2, 10000);
        }

        [Test]
        public void RuminationTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new InnerQuiet());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new Rumination());

            craftTable.AssertStats(1000 - 30, 0, 1139, 6, 10000 - 18 - 18 * 3 + 32);
        }

        [Test]
        public void TricksOfTradeTest()
        {
            var craftTable = TestData.CreateFactory(registry =>
            {
                registry.WithConditions(Condition.Normal, Condition.Normal, Condition.Normal, Condition.Good);
            }).WithDefaults();

            craftTable.Act(new InnerQuiet());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new TricksOfTheTrade());

            craftTable.AssertStats(1000 - 30, 0, 1139, 6, 10000 - 18 - 18 * 3 + 20);
        }

        [Test]
        public void ManipulationTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());

            craftTable.Act(new Manipulation());

            craftTable.Act(new BasicTouch());
            craftTable.Act(new Observe());
            craftTable.Act(new Observe());
            craftTable.Act(new Observe());

            craftTable.AssertStats(1000 - 30 - 10 + 30, 0, 317*4, 9, 10000 - 18 * 4 - 88 - 3*14);
        }

        [Test]
        public void FlawlessSynthesisTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new FlawlessSynthesis());

            craftTable.AssertStats(1000 - 10, 40, 0, 2, 10000 - 15);
        }

        [Test]
        public void MakersMarkTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new MakersMark());
            for (int i = 0; i < 3; i++)
            {
                craftTable.Act(new FlawlessSynthesis());
            }

            craftTable.AssertStats(1000 - 10, 80+40, 0, 5, 10000 - 15 - 20);
        }

        [Test]
        public void MastersMendTest()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new SteadyHand2Ability());
            for (int i = 0; i < 4; i++)
            {
                craftTable.Act(new BasicTouch());
            }
            craftTable.Act(new MastersMend());
            craftTable.Act(new BasicTouch());

            craftTable.AssertStats(1000 - 50+30, 0, 317*5, 8, 10000 - 18*5 - 92-25);
        }


        [Test]
        public void MastersMend2Test()
        {
            var craftTable = TestData.CreateFactory().WithDefaults();

            craftTable.Act(new SteadyHandAbility());
            for (int i = 0; i < 8; i++)
            {
                craftTable.Act(new BasicTouch());
            }
            craftTable.Act(new MastersMend2());
            craftTable.Act(new BasicTouch());

            craftTable.AssertStats(1000 - 90 + 60, 0, 317 * 9, 12, 10000 - 18 * 9 - 160 - 22);
        }

    }
}