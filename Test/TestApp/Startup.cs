using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSwag.AspNetCore;
using TestApp.Logger;
using TestApp.Models;
using WebService.Interfaces;
using WebService.Service;

namespace TestApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();

            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddScoped<IPostsService, PostsService>();
            services.AddScoped<ICommentsService, CommentsService>();
            services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), Configuration["Logging:FileName"]));
            var logger = loggerFactory.CreateLogger(Configuration["Logging:LoggerName"]);

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var errFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var error = errFeature.Error;
                    var errResult = new ErrorModel();
                    if (!(error is ArgumentNullException) && !(error is ArgumentException))
                    {
                        errResult.Title = "Unhandled exception.";
                        errResult.Details = error.Message;
                        errResult.Status = 500;
                        errResult.Type = error.GetType().ToString();
                        errResult.InnerException = error.InnerException.Message;
                    }

                    logger.LogError(errResult.ToString());

                    context.Response.StatusCode = errResult.Status.Value;
                });

            });

            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, new SwaggerUiOwinSettings());
            app.UseMvc();
        }
    }
}
