using DevPlatform.Core;
using DevPlatform.Core.Attributes;
using DevPlatform.Core.Configuration.Configs;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Infrastructure;
using DevPlatform.Core.Security.JwtSecurity;
using DevPlatform.Data;
using DevPlatform.Data.IdentityFactory;
using DevPlatform.Domain.Api;
using DevPlatform.Domain.Api.AlbumApi;
using DevPlatform.Domain.Api.QuestionApi;
using DevPlatform.Domain.Api.StoryApi;
using DevPlatform.Domain.Validation;
using DevPlatform.Domain.Validation.AlbumValidation;
using DevPlatform.Domain.Validation.QuestionValidation;
using DevPlatform.Domain.Validation.StoryValidation;
using DevPlatform.LinqToDB.Identity;
using FluentValidation;
using FluentValidation.AspNetCore;
using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using StackExchange.Profiling.Storage;
using DevPlatform.Business.Configuration.Common;
using DevPlatform.Domain.Enumerations;

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
        public static (IEngine, AppConfigs) ConfigureApplicationServices(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            //most of API providers require TLS 1.2 nowadays
            ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;

            //create default file provider
            CommonHelper.DefaultFileProvider = new DevPlatformFileProvider(webHostEnvironment);

            //add accessor to HttpContext
            services.AddHttpContextAccessor();

            //add configuration parameters
            var appConfigs = new AppConfigs();
            configuration.Bind(appConfigs);
            services.AddSingleton(appConfigs);
            AppConfigsHelper.SaveAppSettings(appConfigs);

            //create engine and configure service provider
            var engine = EngineContext.Create();

            engine.ConfigureServices(services, configuration);
            engine.RegisterDependencies(services, appConfigs);

            return (engine, appConfigs);
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
        /// Create, bind and register as service the specified configuration parameters 
        /// </summary>
        /// <typeparam name="TConfig">Configuration parameters</typeparam>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Set of key/value application configuration properties</param>
        /// <returns>Instance of configuration parameters</returns>
        public static TConfig AddConfig<TConfig>(this IServiceCollection services, IConfiguration configuration)
            where TConfig : class, IConfig, new()
        {
            //create instance of config
            var config = new TConfig();

            //bind it to the appropriate section of configuration
            configuration.Bind(config.Name, config);

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
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "DevPlatformSpa/dist/"; });

            //adding cors
            services.AddCors();

            services.AddMvc(opt =>
            {
                opt.EnableEndpointRouting = false;
                opt.Filters.Add(typeof(ValidateModelAttribute));
            }).SetCompatibilityVersion(CompatibilityVersion.Latest)
            .AddFluentValidation(fvc => { });
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "devPlatform Api", Version = DevPlatformVersion.CurrentVersion });

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
                var appSettings = Singleton<AppConfigs>.Instance;
                var jwtConfig = appSettings.JwtConfig;

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

                services.AddAuthentication().AddCookie(options =>
                 {
                     options.Cookie.Name = "Interop";
                     options.DataProtectionProvider =
                         DataProtectionProvider.Create(new DirectoryInfo("C:\\Github\\Identity\\artifacts"));
                 });

                JwtTokenDefinitions.LoadFromConfiguration(jwtConfig);
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
            services.AddSingleton<IValidator<PostCreateApi>, PostCreateApiValidator>();
            services.AddSingleton<IValidator<AlbumCreateApi>, AlbumCreateApiValidator>();
            services.AddSingleton<IValidator<StoryCreateApi>, StoryCreateApiValidator>();
            services.AddSingleton<IValidator<QuestionCreateApi>, QuestionCreateApiValidator>();
            services.AddSingleton<IValidator<RegisterApiRequest>, RegisterApiRequestValidator>();
        }

        /// <summary>
        /// Adds SignalR
        /// </summary>
        /// <param name="services"></param>
        public static void AddDevPlatFormSignalR(this IServiceCollection services)
        {
            services.AddSignalR();
        }

        /// <summary>
        /// Adds DevPlatform behavior options
        /// </summary>
        /// <param name="services"></param>
        public static void AddDevPlatformBehaviorOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.SuppressModelStateInvalidFilter = true;
            });
        }

        /// <summary>
        /// Add and configure MiniProfiler service
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddDevPlatformMiniProfiler(this IServiceCollection services)
        {
            //whether database is already installed
            if (!DataSettingsManager.IsDatabaseInstalled())
                return;

            services.AddMiniProfiler(miniProfilerOptions =>
            {
                //use memory cache provider for storing each result
                ((MemoryCacheStorage)miniProfilerOptions.Storage).CacheDuration = TimeSpan.FromMinutes(60);

                //whether MiniProfiler should be displayed
                miniProfilerOptions.ShouldProfile = request => true;

                //determine who can access the MiniProfiler results
                miniProfilerOptions.ResultsAuthorize = request => true;
            });
        }

        /// <summary>
        /// Adds services required for distributed cache
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddDistributedCache(this IServiceCollection services)
        {
            var appSettings = Singleton<AppConfigs>.Instance;
            var distributedCacheConfig = appSettings.DistributedCacheConfig;

            if (!distributedCacheConfig.Enabled)
                return;

            switch (distributedCacheConfig.DistributedCacheType)
            {
                case DistributedCacheType.Memory:
                    services.AddDistributedCache();
                    break;

                case DistributedCacheType.SqlServer:
                    services.AddDistributedSqlServerCache(options =>
                    {
                        options.ConnectionString = distributedCacheConfig.ConnectionString;
                        options.SchemaName = distributedCacheConfig.SchemaName;
                        options.TableName = distributedCacheConfig.TableName;
                    });
                    break;

                case DistributedCacheType.Redis:
                    services.AddStackExchangeRedisCache(options =>
                    {
                        options.Configuration = distributedCacheConfig.ConnectionString;
                    });
                    break;
            }
        }
    }
}
