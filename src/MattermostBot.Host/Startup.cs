using System.Text.Json;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Serilog;
using SimpleInjector;

namespace MattermostBot.Host
{
    public class Startup
    {
        private Container _container = new Container();

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Configuration = configuration;
            CurrentEnvironment = environment;
        }

        public Startup(IConfiguration configuration, IWebHostEnvironment currentEnvironment)
        {
            this.Configuration = configuration;
            this.CurrentEnvironment = currentEnvironment;

        }
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment CurrentEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options => 
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });

            services.AddSimpleInjector(_container, options =>
            {
                options.AddAspNetCore()
                    .AddControllerActivation();

                options.AddLogging();
            });

            ConfigureContainer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSimpleInjector(_container);

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSerilogRequestLogging();

            _container.Verify();
        }

        private void ConfigureContainer()
        {
            AddMediatR();
        }

        private void AddMediatR()
        {
            var assemblies = new [] { this.GetType().Assembly };
            _container.RegisterSingleton<IMediator, Mediator>();
            
            _container.Register(typeof(IRequestHandler<,>), assemblies);
            _container.Collection.Register(typeof(INotificationHandler<>), assemblies);

            _container.Collection.Register(typeof(IPipelineBehavior<,>), new []
            {
                typeof(RequestPreProcessorBehavior<,>),
                typeof(RequestPostProcessorBehavior<,>)
            });

            _container.Collection.Register(typeof(IRequestPreProcessor<>));
            _container.Collection.Register(typeof(IRequestPostProcessor<,>));

            _container.RegisterInstance(new ServiceFactory(_container.GetInstance));

        }
    }
}
