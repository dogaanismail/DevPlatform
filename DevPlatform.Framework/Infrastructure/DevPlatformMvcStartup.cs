﻿using DevPlatform.Core.Infrastructure;
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
            //services.AddDevPlatformAuthentication(configuration);
        }

        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
            application.UseDevPlatformEnvironment();

            application.UseDevPlatformExceptionHandler();

            application.UseDevPlatformStaticFiles();

            application.UseDevPlatformAuthentication();

            application.UseAngular();
        }

        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order => 1000; //MVC should be loaded last
    }
}
