using Autofac;
using Data;
using Microsoft.Extensions.Configuration;
using WebService.Interfaces;

namespace WebService
{
    public class AutofacModule : Module
    {
        private readonly IConfiguration _configuration;

        public AutofacModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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
