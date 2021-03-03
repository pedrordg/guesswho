using Autofac;
using GuessWho.SignalR.Contracts;
using GuessWho.SignalR.Hubs;

namespace GuessWho.SignalR.DependencyInjection
{
    public class SignalRModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PlayerBag>().As<IPlayerBag>().SingleInstance();
        }
    }
}
