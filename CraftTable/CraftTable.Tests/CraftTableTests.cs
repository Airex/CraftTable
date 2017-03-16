using CrafterExperiment;
using CrafterExperiment.Abilities;
using CrafterExperiment.Buffs;
using NUnit.Framework;

namespace CrafterExperimentTests
{
    [TestFixture]
    public class CraftTableTests
    {
        [Test]
        public void Test1()
        {
            var craftTable = CreateCraftTable();


            craftTable.Act(new BasicTouch());

            craftTable.AssertStats(1000-10,0,317,2,10000-18);
        }

        [Test]
        public void Test2()
        {
            var craftTable = CreateCraftTable();

            craftTable.Act(new GreatStrides());
            craftTable.Act(new BasicTouch());

            craftTable.AssertStats(1000-10, 0, 635, 3, 10000-50);
        }

        [Test]
        public void Test3()
        {
            var craftTable = CreateCraftTable();

            craftTable.Act(new GreatStrides());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());

            craftTable.AssertStats(1000-40, 0, 635+317*3, 6, 10000-32-4*18);
        }


        [Test]
        public void Test4()
        {
            var craftTable = CreateCraftTable();

            craftTable.Act(new GreatStrides());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());

            craftTable.AssertStats(1000 - 40, 0, 635 + 317 * 3, 6, 10000 - 32 - 4 * 18);
        }

        [Test]
        public void Test5()
        {
            var craftTable = CreateCraftTable();

            craftTable.Act(new InnerQuite());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());


            craftTable.AssertStats(1000 - 50, 0, 2222, 7, 10000 - 18 - 5 * 18);
        }

        [Test]
        public void Test6()
        {
            var craftTable = CreateCraftTable();
            
            craftTable.Act(new BasicTouch());
            craftTable.Act(new StandartTouch());
            craftTable.Act(new AdvancedTouch());


            craftTable.AssertStats(1000 - 30, 0, 1190, 4, 10000 - 18 - 32- 48);
        }

        [Test]
        public void Test7()
        {
            var craftTable = CreateCraftTable();

            craftTable.Act(new Innovation());
            craftTable.Act(new BasicTouch());
           


            craftTable.AssertStats(1000 - 10, 0, 475, 3, 10000 - 18 - 18);
        }

        [Test]
        public void Test8()
        {
            var craftTable = CreateCraftTable();

            craftTable.Act(new Innovation());
            craftTable.Act(new GreatStrides());
            craftTable.Act(new BasicTouch());



            craftTable.AssertStats(1000 - 10, 0, 950, 4, 10000 - 18 - 18-32);
        }

        [Test]
        public void Test9()
        {
            var craftTable = CreateCraftTable();

            craftTable.Act(new ComfortZone());
            for (int i = 0; i < 10; i++)
            {
                craftTable.Act(new BasicTouch());
            }
            craftTable.AssertStats(1000 - 100, 0, 317*10, 12, 10000 - 66 + 80 - 10*18);
        }


        [Test]
        public void Test10()
        {
            var craftTable = CreateCraftTable();

            craftTable.Act(new InnerQuite());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new ByregotBlessing());
            craftTable.AssertStats(1000 - 30, 0, 1316, 5, 10000 - 18*3 - 24);
        }


        [Test]
        public void Test11()
        {
            var craftTable = CreateCraftTable(new Recipe(478,60,3140));

            craftTable.Act(new MuscleMemory());
            
            craftTable.AssertStats(50, 157, 0, 2, 10000 - 6);
        }

        [Test]
        public void Test12()
        {
            var craftTable = CreateCraftTable();

            craftTable.Act(new WasteNot());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.Act(new BasicTouch());
            craftTable.AssertStats(1000 - 15, 0, 317*3, 5, 10000 - 18 * 3 -56);
        }

        [Test]
        public void Test13()
        {
            var craftTable = CreateCraftTable();

            craftTable.Act(new CarefulSynthesis2());
            
            craftTable.AssertStats(1000 - 10, 222, 0, 2, 10000 );
        }

        private static CraftTable CreateCraftTable(Recipe recipe = null)
        {
            Recipe r = recipe ?? new Recipe(2000, 1000, 20000);
            CraftMan craftMan = new CraftMan(788, 851, 10000);

            IRandomService randomService = new StaticRandomService(0);

            IEfficiencyCalculator efficiencyCalculator = new EfficiencyCalculator();

            IBuffCollector buffCollector = new BuffCollector();
            IConditionService conditionService = new ConditionService(randomService);
            ICalculator calculator = new CalculatorBuilder(efficiencyCalculator);

            CraftTable craftTable = new CraftTable(r, craftMan, buffCollector, conditionService, randomService, calculator);
            return craftTable;
        }
    }


    public class StaticRandomService:IRandomService
    {
        private readonly int _value;

        public StaticRandomService(int value)
        {
            _value = value;
        }

        public int SelectItem(int[] chances)
        {
            return _value;
        }
    }
}