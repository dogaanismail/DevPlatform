//using DevPlatform.Core.Infrastructure;
//using DevPlatform.Framework.Infrastructure.Extensions;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace DevPlatform.Framework.Infrastructure
//{
//    /// <summary>
//    /// Represents object for the configuring common features and middleware on application startup
//    /// </summary>
//    public class DevPlatformCommonStartup : IDevPlatformStartup
//    {
//        /// <summary>
//        /// Add and configure any of the middleware
//        /// </summary>
//        /// <param name="services">Collection of service descriptors</param>
//        /// <param name="configuration">Configuration of the application</param>
//        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
//        {
//            //compression
//            services.AddResponseCompression();

//            //add options feature
//            services.AddOptions();

//            //add distributed memory cache
//            services.AddDistributedMemoryCache();

//            //add localization
//            services.AddLocalization();
//        }

//        /// <summary>
//        /// Configure the using of added middleware
//        /// </summary>
//        /// <param name="application">Builder for configuring an application's request pipeline</param>
//        public void Configure(IApplicationBuilder application)
//        {
//            //use static files feature
//            application.UseDevPlatformStaticFiles();
//        }

//        /// <summary>
//        /// Gets order of this startup configuration implementation
//        /// </summary>
//        public int Order => 100; //common services should be loaded after error handlers
//    }
//}
