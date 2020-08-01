//using DevPlatform.Core.Infrastructure;
//using DevPlatform.Framework.Infrastructure.Extensions;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//namespace DevPlatform.Framework.Infrastructure
//{
//    /// <summary>
//    /// Represents object for the configuring authentication middleware on application startup
//    /// </summary>
//    public class AuthenticationStartup : IDevPlatformStartup
//    {
//        /// <summary>
//        /// Add and configure any of the middleware
//        /// </summary>
//        /// <param name="services">Collection of service descriptors</param>
//        /// <param name="configuration">Configuration of the application</param>
//        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
//        {
//            //add authentication
//            services.AddDevPlatformAuthentication();
//        }

//        /// <summary>
//        /// Configure the using of added middleware
//        /// </summary>
//        /// <param name="application">Builder for configuring an application's request pipeline</param>
//        public void Configure(IApplicationBuilder application)
//        {
//            //configure authentication
//            application.UseAuthentication();
//        }

//        /// <summary>
//        /// Gets order of this startup configuration implementation
//        /// </summary>
//        public int Order => 500; //authentication should be loaded before MVC
//    }
//}
