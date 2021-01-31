using Autofac;
using GuessWho.Execution;
using GuessWho.Execution.Contracts;
using GuessWho.Execution.Table;

namespace GuesWho.ExecutionDependencyInjection
{
    public class ExecutionModule : Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IdolCrud>().As<IIdolCrud>().InstancePerLifetimeScope();
            builder.RegisterType<IdolFetcher>().As<IIdolFetcher>().InstancePerLifetimeScope();
        }
    }
}
