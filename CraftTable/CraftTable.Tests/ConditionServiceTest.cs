﻿using System;
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

            IList<Condition> conditions = new List<Condition>();
            for (var i = 0; i < 100000; i++)
            {
                var condition = conditionService.GetCondition();
                conditions.Add(condition);
            }

            var a = from c in conditions
                group c by c
                into g
                select new {Condition = g.Key, Count = g.Count(), Percent = (double) g.Count() / conditions.Count};
            Console.WriteLine($"Condition: \t Count \t\t\t %");
            foreach (var c in a)
            {
                Console.WriteLine($"{c.Condition}\t\t {c.Count} \t\t\t {c.Percent*100}");
            }
        }
    }
}