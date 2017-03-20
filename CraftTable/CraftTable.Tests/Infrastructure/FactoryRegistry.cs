using Autofac;

namespace CraftTable.Tests.Infrastructure
{
    internal class FactoryRegistry : IFactoryRegistry
    {
        public FactoryRegistry(ContainerBuilder builder)
        {
            Builder = builder;
        }

        public ContainerBuilder Builder { get; }
    }
}