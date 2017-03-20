using System.Linq;
using Autofac;
using CraftTable.Contracts;

namespace CraftTable.Tests.Infrastructure
{
    internal static class FactoryRegistrations
    {
        internal static IFactoryRegistry WithConditions(this IFactoryRegistry factoryRegistry,
            params Condition[] conditions)
        {
            factoryRegistry.Builder.Register(
                context => new ConditionService(new StaticRandomService(conditions.Select(c => (int) c).ToArray()))).As<IConditionService>();
            return factoryRegistry;
        }
    }
}