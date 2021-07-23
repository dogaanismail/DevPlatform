using DevPlatform.Core.Infrastructure;
using DevPlatform.Framework.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevPlatform.Framework.Infrastructure
{
    /// <summary>
    /// Represents object for the configuring MVC on application startup
    /// </summary>
    public class DevPlatformMvcStartup : IDevPlatformStartup
    {
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //add mvc options and settings
            services.AddDevPlatformMvc();

            //add options feature
            services.AddOptions();

            //add Fluent Validation validator
            services.AddMyValidator();

            //add devPlatform Identity options
            services.AddDevPlatformAuthentication(configuration);

            //add devPlatform swagger
            services.AddDevPlatformSwagger();

            //add devPlatform swagger
            services.AddDevPlatFormSignalR();

            //custom states errors
            services.AddDevPlatformBehaviorOptions();

            //add memory cache
            services.AddDistributedCache();

            //add MiniProfiler services
            services.AddDevPlatformMiniProfiler();
        }

        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
            //used for environment
            application.UseDevPlatformEnvironment();

            //used for exceptionhandler
            application.UseDevPlatformExceptionHandler();

            //used for static files
            application.UseDevPlatformStaticFiles();

            //used for routing
            application.UseDevPlatformRouting();

            //used for authentication
            application.UseDevPlatformAuthentication();

            //used for authorization
            application.UseDevPlatformAuthorization();

            //used for swagger
            application.UseDevPlatformSwagger();

            //used for angular
            application.UseAngular();

            //used for signalR
            application.AddDevPlatformSignalR();

            //use MiniProfiler
            application.UseMiniProfiler();
        }

        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order => 1000; //MVC should be loaded last
    }
}
