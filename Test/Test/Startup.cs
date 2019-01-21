using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebService
{
    public class Startup
    {
        public IContainer AppContainer { get; private set; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Configure(IApplicationBuilder appBuilder, IApplicationLifetime appLifetime)
        {
            appBuilder.UseDefaultFiles();
            appBuilder.UseStaticFiles();

            appLifetime.ApplicationStopped.Register(() => AppContainer.Dispose());
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(connectionString));

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new AutofacModule());
            AppContainer = builder.Build();
            return new AutofacServiceProvider(AppContainer);
        }
    }
}