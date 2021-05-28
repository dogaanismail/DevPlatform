using DevPlatform.Business.Interfaces;
using DevPlatform.Business.Services;
using DevPlatform.Core;
using DevPlatform.Core.ComponentModel;
using DevPlatform.Core.Configuration;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Infrastructure;
using DevPlatform.Core.Security;
using DevPlatform.Data;
using DevPlatform.Data.IdentityFactory;
using DevPlatform.Data.Migrations;
using DevPlatform.ImageProcessingLibrary.Contract;
using DevPlatform.ImageProcessingLibrary.Providers;
using DevPlatform.LinqToDB.Identity;
using DevPlatform.Repository.Generic;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Conventions;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace DevPlatform.Tests
{
    /// <summary>
    /// DevPlatform base test abstract class implementations
    /// </summary>
    public abstract class BaseDevPlatformTest
    {
        private static readonly ServiceProvider _serviceProvider;

        static BaseDevPlatformTest()
        {
            TypeDescriptor.AddAttributes(typeof(List<int>),
                new TypeConverterAttribute(typeof(GenericListTypeConverter<int>)));
            TypeDescriptor.AddAttributes(typeof(List<string>),
                new TypeConverterAttribute(typeof(GenericListTypeConverter<string>)));

            var services = new ServiceCollection();

            services.AddHttpClient();

            var typeFinder = new AppDomainTypeFinder();

            Singleton<DataSettings>.Instance = new DataSettings
            {
                ConnectionString = "Data Source=DESKTOP-STEV1LL\\SQLEXPRESS;Initial Catalog=DevPlatformDBTest;Integrated Security=True"
            };

            var mAssemblies = typeFinder.FindClassesOfType<AutoReversingMigration>()
                .Select(t => t.Assembly)
                .Distinct()
                .ToArray();

            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();

            //add configuration parameters
            var appSettings = new DevPlatformConfig();
            services.AddSingleton(appSettings);
            Singleton<DevPlatformConfig>.Instance = appSettings;

            var cloudinaryConfig = new CloudinaryConfig();
            services.AddSingleton(cloudinaryConfig);
            Singleton<CloudinaryConfig>.Instance = cloudinaryConfig;

            var hostApplicationLifetime = new Mock<IHostApplicationLifetime>();
            services.AddSingleton(hostApplicationLifetime.Object);

            var rootPath =
              new DirectoryInfo(
                      $@"{Directory.GetCurrentDirectory().Split("bin")[0]}{Path.Combine(@"\..\DevPlatform.Api".Split('\\', '/').ToArray())}")
                  .FullName;

            //Applications\DevPlatform.Api
            var webHostEnvironment = new Mock<IWebHostEnvironment>();
            webHostEnvironment.Setup(p => p.WebRootPath).Returns(Path.Combine(rootPath, "wwwroot"));
            webHostEnvironment.Setup(p => p.ContentRootPath).Returns(rootPath);
            webHostEnvironment.Setup(p => p.EnvironmentName).Returns("test");
            webHostEnvironment.Setup(p => p.ApplicationName).Returns("devPlatform");
            services.AddSingleton(webHostEnvironment.Object);

            CommonHelper.DefaultFileProvider = new DevPlatformFileProvider(webHostEnvironment.Object);

            var httpContext = new DefaultHttpContext
            {
                Request = { Headers = { { HeaderNames.Host, DevPlatformTestsDefaults.HostIpAddress } } }
            };

            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(p => p.HttpContext).Returns(httpContext);

            services.AddSingleton(httpContextAccessor.Object);

            var actionContextAccessor = new Mock<IActionContextAccessor>();
            actionContextAccessor.Setup(x => x.ActionContext)
                .Returns(new ActionContext(httpContext, httpContext.GetRouteData(), new ActionDescriptor()));

            services.AddSingleton(actionContextAccessor.Object);

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            var urlHelper = new DevTestUrlHelper(actionContextAccessor.Object.ActionContext);

            urlHelperFactory.Setup(x => x.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(urlHelper);

            services.AddTransient(provider => actionContextAccessor.Object);

            services.AddSingleton(urlHelperFactory.Object);

            var tempDataDictionaryFactory = new Mock<ITempDataDictionaryFactory>();
            var dataDictionary = new TempDataDictionary(httpContextAccessor.Object.HttpContext,
                new Mock<ITempDataProvider>().Object);
            tempDataDictionaryFactory.Setup(f => f.GetTempData(It.IsAny<HttpContext>())).Returns(dataDictionary);
            services.AddSingleton(tempDataDictionaryFactory.Object);

            services.AddSingleton<ITypeFinder>(typeFinder);

            //file provider
            services.AddTransient<IDevPlatformFileProvider, DevPlatformFileProvider>();

            //web helper
            services.AddTransient<IWebHelper, WebHelper>();

            //data layer
            services.AddTransient<IDataProviderManager, TestDataProviderManager>();
            services.AddTransient<IDevPlatformDataProvider, MsSqlDataProviderTest>();

            //repositories
            services.AddTransient(typeof(IRepository<>), typeof(RepositoryBase<>));

            //services
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IPostImageService, PostImageService>();
            services.AddTransient<IPostVideoService, PostVideoService>();
            services.AddTransient<IPostCommentService, PostCommentService>();
            services.AddTransient<IStoryService, StoryService>();
            services.AddTransient<IStoryImageService, StoryImageService>();
            services.AddTransient<IStoryVideoService, StoryVideoService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserDetailService, UserDetailService>();
            services.AddTransient<IChatService, ChatService>();
            services.AddTransient<IChatGroupUserService, ChatGroupUserService>();
            services.AddTransient<IChatGroupService, ChatGroupService>();
            services.AddTransient<ISettingService, SettingService>();
            services.AddTransient<IAlbumService, AlbumService>();
            services.AddTransient<IImageProcessingService, ImageProcessingService>();
            services.AddTransient<IDatabaseService, DatabaseService>();
            services.AddTransient<ILogService, LogService>();

            #region ImageProcessingLibrary services
            services.AddScoped<IImageProcessorService, ImageProcessorService>();
            services.AddTransient<IFileProcessorCreater, FileProcessorCreater>();
            services.AddTransient<IFileProcessorClient, ProcessorClient>();
            #endregion

            //register all settings
            var settings = typeFinder.FindClassesOfType(typeof(ISettings), false).ToList();
            foreach (var setting in settings)
                services.AddTransient(setting,
                    context => context.GetRequiredService<ISettingService>().LoadSetting(setting));

            services
              // add common FluentMigrator services
              .AddFluentMigratorCore()
              .AddScoped<IProcessorAccessor, TestProcessorAccessor>()
              .AddScoped<IConnectionStringAccessor>(x => DataSettingsManager.LoadSettings())
              .AddScoped<IMigrationManager, MigrationManager>()
              .AddSingleton<IConventionSet, DevTestConventionSet>()
              .ConfigureRunner(rb =>
                  rb.WithVersionTable(new MigrationVersionInfo()).AddSqlServer()
                      .ScanIn(mAssemblies).For.Migrations());

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;

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

            _serviceProvider = services.BuildServiceProvider();

            EngineContext.Replace(new DevTestEngine(_serviceProvider));

            _serviceProvider.GetService<IDevPlatformDataProvider>().CreateDatabase(null);
            _serviceProvider.GetService<IDevPlatformDataProvider>().InitializeDatabase();
        }

        public T GetService<T>()
        {
            try
            {
                return _serviceProvider.GetRequiredService<T>();
            }
            catch (InvalidOperationException ex)
            {
                return (T)EngineContext.Current.ResolveUnregistered(typeof(T));
            }
        }

        #region Nested classes

        protected class DevTestUrlHelper : UrlHelperBase
        {
            public DevTestUrlHelper(ActionContext actionContext) : base(actionContext)
            {
            }

            public override string Action(UrlActionContext actionContext)
            {
                return string.Empty;
            }

            public override string RouteUrl(UrlRouteContext routeContext)
            {
                return string.Empty;
            }
        }

        protected class DevTestConventionSet : DevPlatformConventionSet
        {
            public DevTestConventionSet(IDevPlatformDataProvider dataProvider) : base(dataProvider)
            {
            }
        }

        public partial class DevTestEngine : DevPlatformEngine
        {
            protected readonly IServiceProvider _internalServiceProvider;

            public DevTestEngine(IServiceProvider serviceProvider)
            {
                _internalServiceProvider = serviceProvider;
            }

            public override IServiceProvider ServiceProvider => _internalServiceProvider;
        }


        #endregion
    }
}
