using Autofac;
using Microsoft.Extensions.Configuration;
using WebService.Interfaces;

namespace WebService
{
    public class AutofacModule : Module
    {
        public AutofacModule() { }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Service.Service>()
                .As<IService>()
                .InstancePerLifetimeScope();

        }
    }
}
