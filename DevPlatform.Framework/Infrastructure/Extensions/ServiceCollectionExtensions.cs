using DevPlatform.Core;
using DevPlatform.Core.Configuration;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Infrastructure;
using DevPlatform.Data;
using LinqToDB.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace DevPlatform.Framework.Infrastructure.Extensions
{
    /// <summary>
    /// Represents extensions of IServiceCollection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add services to the application and configure service provider
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        /// <param name="webHostEnvironment">Hosting environment</param>
        /// <returns>Configured service provider</returns>
        public static (IEngine, DevPlatformConfig) ConfigureApplicationServices(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            //most of API providers require TLS 1.2 nowadays
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //add NopConfig configuration parameters
            var nopConfig = services.ConfigureStartupConfig<DevPlatformConfig>(configuration.GetSection("devPlatform"));

            //add hosting configuration parameters
            services.ConfigureStartupConfig<HostingConfig>(configuration.GetSection("Hosting"));

            //add accessor to HttpContext
            services.AddHttpContextAccessor();

            //create default file provider
            CommonHelper.DefaultFileProvider = new DevPlatformFileProvider(webHostEnvironment);

            //create engine and configure service provider
            var engine = EngineContext.Create();

            engine.ConfigureServices(services, configuration, nopConfig);

            return (engine, nopConfig);
        }

        /// <summary>
        /// Create, bind and register as service the specified configuration parameters 
        /// </summary>
        /// <typeparam name="TConfig">Configuration parameters</typeparam>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Set of key/value application configuration properties</param>
        /// <returns>Instance of configuration parameters</returns>
        public static TConfig ConfigureStartupConfig<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            //create instance of config
            var config = new TConfig();

            //bind it to the appropriate section of configuration
            configuration.Bind(config);

            //and register it as a service
            services.AddSingleton(config);

            return config;
        }


        /// <summary>
        /// Add and configure MVC for the application
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <returns>A builder for configuring MVC services</returns>
        public static IMvcBuilder AddDevPlatformMvc(this IServiceCollection services)
        {
            //add basic MVC feature
            var mvcBuilder = services.AddControllersWithViews();

            services.AddResponseCompression(
               options => options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                               {
                                    "image/jpeg",
                                    "image/png",
                                    "image/gif"
                               }));

            //Angular
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist/"; });

            services.AddMvc(opt =>
            {
                opt.EnableEndpointRouting = false;
                //opt.Filters.Add(typeof(ValidateModelAttribute));
            });
            //.AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());

            //register controllers as services, it'll allow to override them
            mvcBuilder.AddControllersAsServices();

            return mvcBuilder;
        }

        /// <summary>
        /// Register HttpContextAccessor
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        /// <summary>
        /// Adds authentication service
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddDevPlatformAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, LinqToDB.Identity.IdentityRole<int>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;

                options.User.RequireUniqueEmail = true;

                //TODO
                //options.User.RequireUniqueEmail = true; 
                //options.SignIn.RequireConfirmedEmail = true;
                //options.Lockout.MaxFailedAccessAttempts = 5;
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);

            }).AddLinqToDBStores(new DefaultConnectionFactory())
                .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            // Uncomment the following lines to enable logging in with third party login providers

            //JwtTokenDefinitions.LoadFromConfiguration(configuration);
            //services.ConfigureJwtAuthentication();
            //services.ConfigureJwtAuthorization();
        }


    }
}
