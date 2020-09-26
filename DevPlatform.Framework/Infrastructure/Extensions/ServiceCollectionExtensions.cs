using DevPlatform.Core;
using DevPlatform.Core.Attributes;
using DevPlatform.Core.Configuration;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Infrastructure;
using DevPlatform.Core.Security.JwtSecurity;
using DevPlatform.Data;
using DevPlatform.Data.IdentityFactory;
using DevPlatform.Domain.Api;
using DevPlatform.Domain.Validation;
using DevPlatform.LinqToDB.Identity;
using FluentValidation;
using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

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

            //add DevPlatformConfig configuration parameters
            var devPlatformConfig = services.ConfigureStartupConfig<DevPlatformConfig>(configuration.GetSection("devPlatform"));

            //add hosting configuration parameters
            services.ConfigureStartupConfig<HostingConfig>(configuration.GetSection("Hosting"));

            //add cloudinary configuration parameters
            services.ConfigureStartupConfig<CloudinaryConfig>(configuration.GetSection("CloudinarySettings"));

            //add accessor to HttpContext
            services.AddHttpContextAccessor();

            //create default file provider
            CommonHelper.DefaultFileProvider = new DevPlatformFileProvider(webHostEnvironment);

            //create engine and configure service provider
            var engine = EngineContext.Create();

            engine.ConfigureServices(services, configuration, devPlatformConfig);

            return (engine, devPlatformConfig);
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
        public static void AddDevPlatformMvc(this IServiceCollection services)
        {
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
                opt.Filters.Add(typeof(ValidateModelAttribute));
            });
            //.AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());
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
        /// Register HttpContextAccessor
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddDevPlatformSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "devPlatform Api", Version = "1.0" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer", //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });
        }

        /// <summary>
        /// Adds authentication service
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddDevPlatformAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;

                //TODO: The identity might be configured.
                //options.User.RequireUniqueEmail = true; 
                //options.Lockout.MaxFailedAccessAttempts = 5;
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);

            }).AddLinqToDBStores<int, AppUserClaim, AppUserRole, AppUserLogin, AppUserToken, AppRoleClaim>(new
            IdentityConnectionFactory(new SqlServerDataProvider(ProviderName.SqlServer, SqlServerVersion.v2017), "SqlServerIdentity", DataSettingsManager.LoadSettings().ConnectionString))
            .AddUserStore<LinqToDB.Identity.UserStore<int, AppUser, AppRole, AppUserClaim, AppUserRole, AppUserLogin, AppUserToken>>()
            .AddUserManager<UserManager<AppUser>>()
            .AddRoleManager<RoleManager<AppRole>>()
                .AddDefaultTokenProviders();

            services.AddAuthentication()
             .AddCookie(options =>
             {
                 options.Cookie.Name = "Interop";
                 options.DataProtectionProvider =
                     DataProtectionProvider.Create(new DirectoryInfo("C:\\Github\\Identity\\artifacts"));
             });

            // Uncomment the following lines to enable logging in with third party login providers

            JwtTokenDefinitions.LoadFromConfiguration(configuration);
            services.ConfigureJwtAuthentication();
            services.ConfigureJwtAuthorization();
        }

        /// <summary>
        /// Adds the validators
        /// </summary>
        /// <param name="services"></param>
        public static void AddMyValidator(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<LoginApiRequest>, LoginApiRequestValidator>();
            //services.AddSingleton<IValidator<PostCreateApi>, PostCreateApiValidator>();
        }

    }
}
