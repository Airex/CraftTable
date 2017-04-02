using Autofac;
using CraftTable.Contracts;

namespace CraftTable
{

    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(GetType().Assembly)
                .Where(type => type.BaseType == typeof(Ability) && !type.IsNested)
                .Named<Ability>(type => type.Name)
                .As<Ability>();

            builder.RegisterType<EfficiencyCalculator>().As<IEfficiencyCalculator>();
            builder.RegisterType<BuffCollector>().As<IBuffCollector>();
            builder.RegisterType<ConditionService>().As<IConditionService>();
            builder.Register(context => new RandomService()).As<IRandomService>().InstancePerRequest();
            builder.RegisterType<Calculator>().As<ICalculator>().InstancePerDependency();
            builder.RegisterType<LookupService>().As<ILookupService>();
            builder.RegisterType<CraftTable>().AsSelf().InstancePerDependency();
            builder.RegisterType<CraftQualityCalculator>().As<ICraftQualityCalculator>();
        }
    }
}