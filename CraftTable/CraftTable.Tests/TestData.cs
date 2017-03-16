using System;
using Autofac;
using CraftTable.Contracts;

namespace CraftTable.Tests
{
    internal static class TestData
    {
        internal static CraftTable CreateCraftTable(Recipe recipe = null)
        {
            var r = recipe ?? new Recipe(2000, 1000, 20000);
            var craftMan = new CraftMan(788, 851, 10000);

            IRandomService randomService = new StaticRandomService(0);

            IEfficiencyCalculator efficiencyCalculator = new EfficiencyCalculator();

            IBuffCollector buffCollector = new BuffCollector();
            IConditionService conditionService = new ConditionService(randomService);
            ICalculator calculator = new Calculator(efficiencyCalculator);

            return new CraftTable(r, craftMan, buffCollector, conditionService, randomService, calculator);
        }

        internal static CraftTable.Factory CreateFactory(Action<IFactoryRegistry> action = null)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new RegistrationModule());
            builder.Register(context => new StaticRandomService(0)).As<IRandomService>();
            action?.Invoke(new FactoryRegistry(builder));
            var container = builder.Build();
            return container.Resolve<CraftTable.Factory>();
        }

        internal static CraftTable WithDefaults(this CraftTable.Factory f)
        {
            return f(DefaultRecipe, DefatltCraftMAn);
        }

        internal static Recipe DefaultRecipe { get; } = new Recipe(2000, 1000, 20000);
        internal static CraftMan DefatltCraftMAn { get; } = new CraftMan(788, 851, 10000);
    }
}