using DevPlatform.Business.Common.Events;
using DevPlatform.Business.Interfaces.Album;
using DevPlatform.Business.Interfaces.Chat;
using DevPlatform.Business.Interfaces.Common;
using DevPlatform.Business.Interfaces.Configuration;
using DevPlatform.Business.Interfaces.Identity;
using DevPlatform.Business.Interfaces.Logging;
using DevPlatform.Business.Interfaces.Portal;
using DevPlatform.Business.Interfaces.Question;
using DevPlatform.Business.Interfaces.Story;
using DevPlatform.Business.Services.Album;
using DevPlatform.Business.Services.Chat;
using DevPlatform.Business.Services.Common;
using DevPlatform.Business.Services.Configuration;
using DevPlatform.Business.Services.Identity;
using DevPlatform.Business.Services.Logging;
using DevPlatform.Business.Services.Portal;
using DevPlatform.Business.Services.Question;
using DevPlatform.Business.Services.Story;
using DevPlatform.Core;
using DevPlatform.Core.Caching;
using DevPlatform.Core.Configuration.Configs;
using DevPlatform.Core.Configuration.Settings;
using DevPlatform.Core.Events;
using DevPlatform.Core.Infrastructure;
using DevPlatform.Core.Infrastructure.DependencyManagement;
using DevPlatform.Core.Security;
using DevPlatform.Data;
using DevPlatform.ImageProcessingLibrary.Contract;
using DevPlatform.ImageProcessingLibrary.Providers;
using DevPlatform.Repository.Generic;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DevPlatform.Framework.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="typeFinder"></param>
        /// <param name="config"></param>
        public void Register(IServiceCollection services, ITypeFinder typeFinder, AppConfigs appConfigs)
        {
            services.AddScoped<IDevPlatformFileProvider, DevPlatformFileProvider>();

            //data layer
            services.AddTransient<IDataProviderManager, DataProviderManager>();
            services.AddTransient(serviceProvider =>
                serviceProvider.GetRequiredService<IDataProviderManager>().DataProvider);

            //repositories
            services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));

            //web helper
            services.AddScoped<IWebHelper, WebHelper>();

            //static cache manager
            if (appConfigs.DistributedCacheConfig.Enabled)
            {
                services.AddScoped<ILocker, DistributedCacheManager>();
                services.AddScoped<IStaticCacheManager, DistributedCacheManager>();
            }
            else
            {
                services.AddSingleton<ILocker, MemoryCacheManager>();
                services.AddSingleton<IStaticCacheManager, MemoryCacheManager>();
            }

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostImageService, PostImageService>();
            services.AddScoped<IPostVideoService, PostVideoService>();
            services.AddScoped<IPostCommentService, PostCommentService>();
            services.AddScoped<IStoryService, StoryService>();
            services.AddScoped<IStoryImageService, StoryImageService>();
            services.AddScoped<IStoryVideoService, StoryVideoService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserDetailService, UserDetailService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IChatGroupUserService, ChatGroupUserService>();
            services.AddScoped<IChatGroupService, ChatGroupService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<IImageProcessingService, ImageProcessingService>();
            services.AddScoped<IDatabaseService, DatabaseService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IQuestionCommentService, QuestionCommentService>();
            services.AddScoped<IGeoLookupService, GeoLookupService>();
            services.AddScoped<IOpenWeatherService, OpenWeatherService>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IEventPublisher, EventPublisher>();

            #region ImageProcessingLibrary services
            services.AddScoped<IImageProcessorService, ImageProcessorService>();
            services.AddScoped<IFileProcessorCreater, FileProcessorCreater>();
            services.AddScoped<IFileProcessorClient, ProcessorClient>();

            #endregion

            //register all settings
            var settings = typeFinder.FindClassesOfType(typeof(ISettings), false).ToList();
            foreach (var setting in settings)
            {
                services.AddScoped(setting, serviceProvider =>
                {
                    return serviceProvider.GetRequiredService<ISettingService>().LoadSettingAsync(setting).Result;
                });
            }

            //event consumers
            var consumers = typeFinder.FindClassesOfType(typeof(IConsumer<>)).ToList();
            foreach (var consumer in consumers)
                foreach (var findInterface in consumer.FindInterfaces((type, criteria) =>
                {
                    var isMatch = type.IsGenericType && ((Type)criteria).IsAssignableFrom(type.GetGenericTypeDefinition());
                    return isMatch;
                }, typeof(IConsumer<>)))
                    services.AddScoped(findInterface, consumer);
        }

        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        public int Order => 0;
    }
}
