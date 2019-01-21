using Autofac;
using Data;
using WebService.Interfaces;

namespace WebService
{
    public class AutofacModule : Module
    {
        public AutofacModule() { }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Repository>()
                .As<IRepository>()
                .SingleInstance();

            builder.RegisterType<Service.Service>()
                .As<IService>()
                .InstancePerLifetimeScope();
        }
    }
}
