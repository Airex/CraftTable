using Autofac;

namespace CraftTable.Tests
{
    internal interface IFactoryRegistry
    {
        ContainerBuilder Builder { get; }
    }
}