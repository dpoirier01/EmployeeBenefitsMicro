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
using Microsoft.EntityFrameworkCore;
using EmployeeBenefits.Business;
using Microsoft.Data.Sqlite;

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

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                   .AllowAnyMethod()
                                                                    .AllowAnyHeader()));

            services.AddSingleton(_config);

            services.AddAutoMapper();
            
            services.AddTransient<IMediator, Mediator>();

            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            services.AddDbContext<BenefitsContext>();

            services.AddScoped<IBenefitsContext>(provider => provider.GetService<BenefitsContext>());
            
            services.AddTransient<ISummarizeBenefits, SummarizeBenefits>();
            services.AddTransient<IDeterminePromotions, DeterminePromotions>();
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
            //container.Register(_serviceProvider.GetService<ISummarizeBenefits>());
            //container.Register(_serviceProvider.GetService<IProcess<BenefitsSummary>>());
            //container.Resolve<ITaskFactory>();
            //container.Register(_serviceProvider.GetService<ITaskFactory>());
            //container.Register(_serviceProvider.GetService<BenefitsSummary>());
           
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);
            
            container.Register(typeof(ISummarizeBenefits), typeof(SummarizeBenefits));
        }
    }
}
