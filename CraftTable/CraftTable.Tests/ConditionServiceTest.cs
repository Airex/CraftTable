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
            var container = builder.Build();

            var conditionService = container.Resolve<IConditionService>();
            var calculator = container.Resolve<ICalculator>();

            calculator.GetBuilder().ForConditionChance((condition, chance) =>
            {
                if (condition == Condition.Extreme)
                    chance.Add(5);
                if (condition == Condition.Good)
                    chance.Add(10);
            });

            IList<Condition> conditions = new List<Condition>();
            for (var i = 0; i < 100000; i++)
            {
                conditions.Add(conditionService.GetCondition(calculator));
            }

            var a = from c in conditions
                group c by c
                into g
                select new {Condition = g.Key, Count = g.Count(), Percent = (double) g.Count() / conditions.Count};
            Console.WriteLine($"Condition: \t Count \t\t %");
            foreach (var c in a.OrderBy(arg => arg.Condition))
            {
                Console.WriteLine($"{c.Condition}\t\t {c.Count} \t\t {c.Percent*100}");
            }
        }
    }
}