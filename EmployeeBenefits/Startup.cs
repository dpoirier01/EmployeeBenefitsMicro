using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nancy.Owin;
using AutoMapper;
using MediatR;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using EmployeeBenefits.Commands.Handlers;
using Nancy;
using Nancy.Configuration;
using Nancy.TinyIoc;
using EmployeeBenefits.Data;
using EmployeeBenefits.Data.Repositories;

namespace EmployeeBenefits
{
    public class Startup
    {
        private IConfigurationRoot _config;
        private IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("appSettings.json")
                .AddEnvironmentVariables();

            _config = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                   .AllowAnyMethod()
                                                                    .AllowAnyHeader()));

            services.AddSingleton(_config);

            services.AddAutoMapper();

            //services.AddScoped<IMediator, Mediator>();
            //services.AddTransient<SingleInstanceFactory>(sp => t => sp.GetService(t));
            //services.AddTransient<MultiInstanceFactory>(sp => t => sp.GetServices(t));
            //services.AddMediatorHandlers(typeof(Startup).GetTypeInfo().Assembly);

            services.AddTransient<IMediator, Mediator>();

            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            services.AddDbContext<BenefitsContext>();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors("AllowAll");

            loggerFactory.AddConsole();

            app.UseOwin(x => x.UseNancy(options =>
            {
                options.Bootstrapper = new Bootstrapper(app.ApplicationServices);
            }));
        }
    }

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        readonly IServiceProvider _serviceProvider;

        public Bootstrapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override void Configure(INancyEnvironment environment)
        {
            environment.Tracing(true, true);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            // DI registrations
            container.Register(_serviceProvider.GetService<ILoggerFactory>());
            container.Register(_serviceProvider.GetService<IMediator>());
            container.Register(_serviceProvider.GetService<IMapper>());
        }
    }

    //public static class MediatorExtensions
    //{
    //    public static IServiceCollection AddMediatorHandlers(this IServiceCollection services, Assembly assembly)
    //    {
    //        var classTypes = assembly.ExportedTypes.Select(t => t.GetTypeInfo()).Where(t => t.IsClass && !t.IsAbstract);

    //        foreach (var type in classTypes)
    //        {
    //            var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

    //            foreach (var handlerType in interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
    //            {
    //                services.AddTransient(handlerType.AsType(), type.AsType());
    //            }

    //            foreach (var handlerType in interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IAsyncRequestHandler<,>)))
    //            {
    //                services.AddTransient(handlerType.AsType(), type.AsType());
    //            }
    //        }

    //        return services;
    //    }
    //}
}
