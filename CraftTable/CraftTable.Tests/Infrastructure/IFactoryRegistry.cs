using Autofac;

namespace CraftTable.Tests.Infrastructure
{
    internal interface IFactoryRegistry
    {
        ContainerBuilder Builder { get; }
    }
}