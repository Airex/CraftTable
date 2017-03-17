using Autofac;
using CraftTable.Contracts;

namespace CraftTable
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfficiencyCalculator>().As<IEfficiencyCalculator>();
            builder.RegisterType<BuffCollector>().As<IBuffCollector>();
            builder.RegisterType<ConditionService>().As<IConditionService>();
            builder.Register(context => new RandomService()).As<IRandomService>();
            builder.RegisterType<Calculator>().As<ICalculator>();
            builder.RegisterType<LookupService>().As<ILookupService>();
            builder.RegisterType<CraftTable>().AsSelf();
        }
    }
}