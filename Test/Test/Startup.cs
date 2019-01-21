using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebService
{
    public class Startup
    {
        public IContainer AppContainer { get; private set; }

        public Startup() { }

        public void Configure(IApplicationBuilder appBuilder, IApplicationLifetime appLifetime)
        {
            appBuilder.UseDefaultFiles();
            appBuilder.UseStaticFiles();

            appLifetime.ApplicationStopped.Register(() => AppContainer.Dispose());
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new AutofacModule());

            AppContainer = builder.Build();

            return new AutofacServiceProvider(AppContainer);
        }
    }
}