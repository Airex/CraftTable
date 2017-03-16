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
            builder.Register(context => (IRandomService) null).As<IRandomService>();
            builder.RegisterType<Calculator>().As<ICalculator>();
            builder.RegisterType<CraftTable>().AsSelf();
        }
    }
}