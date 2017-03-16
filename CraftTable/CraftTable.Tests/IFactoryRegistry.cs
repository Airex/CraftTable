using Autofac;

namespace CraftTable.Tests
{
    internal interface IFactoryRegistry
    {
        ContainerBuilder Builder { get; }
    }

    internal class FactoryRegistry : IFactoryRegistry
    {
        public FactoryRegistry(ContainerBuilder builder)
        {
            Builder = builder;
        }

        public ContainerBuilder Builder { get; }
    }
}