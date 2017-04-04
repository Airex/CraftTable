using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using CraftTable.Contracts;
using NUnit.Framework;

namespace CraftTable.Tests
{
    public class ConditionServiceTest
    {
        [Test]
        public void DistributionTest()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new RegistrationModule());
            builder.Register(context => new RandomService()).As<IRandomService>();
            var container = builder.Build();

            var conditionService = container.Resolve<IConditionService>();
            var calculator = container.Resolve<ICalculator>();

            calculator.GetBuilder().ForConditionChance((condition, chance) =>
            {
                if (condition == Condition.Excellent)
                    chance.Add(0);
                if (condition == Condition.Good)
                    chance.Add(0);
            });

            IList<Condition> conditions = new List<Condition>();
            Condition? cnd = null;
            for (var i = 0; i < 100000; i++)
            {
                cnd = conditionService.GetCondition(cnd);
                conditions.Add(cnd.Value);
            }

            var a = from c in conditions
                group c by c
                into g
                select new {Condition = g.Key, Count = g.Count(), Percent = (double) g.Count() / conditions.Count};
            Console.WriteLine($"Ability: \t Count \t\t %");
            foreach (var c in a.OrderBy(arg => arg.Condition))
            {
                Console.WriteLine($"{c.Condition}\t\t {c.Count} \t\t {c.Percent*100}");
            }
        }

        [Test]
        public void AbilityDistributionTest()
        {
            Random random = new Random();

            int abilitiesCount = 13;

            IList<int> list = new List<int>();

            for (var i = 0; i < 1000000; i++)
            {
                var nextDouble = random.NextDouble();
                list.Add( Math.Min((int)(nextDouble*abilitiesCount),abilitiesCount-1));
            }

            var a = from c in list
                    group c by c
                into g
                    select new { Ability = g.Key, Count = g.Count(), Percent = (double)g.Count() / list.Count };
            Console.WriteLine($"Ability: \t Count \t\t %");
            foreach (var c in a.OrderBy(arg => arg.Ability))
            {
                Console.WriteLine($"{c.Ability}\t\t {c.Count} \t\t {c.Percent * 100}");
            }
        }
    }
}