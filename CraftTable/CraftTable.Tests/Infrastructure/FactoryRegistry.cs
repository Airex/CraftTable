using Autofac;

namespace CraftTable.Tests
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