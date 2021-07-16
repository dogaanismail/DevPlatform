﻿using Autofac;
using DevPlatform.Business.Interfaces;
using DevPlatform.Business.Services;
using DevPlatform.Core;
using DevPlatform.Core.Configuration;
using DevPlatform.Core.Infrastructure;
using DevPlatform.Core.Infrastructure.DependencyManagement;
using DevPlatform.Core.Security;
using DevPlatform.Data;
using DevPlatform.ImageProcessingLibrary.Contract;
using DevPlatform.ImageProcessingLibrary.Providers;
using DevPlatform.Repository.Generic;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
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
        public void Register(IServiceCollection services, ITypeFinder typeFinder, DevPlatformConfig config)
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
                    return serviceProvider.GetRequiredService<ISettingService>().LoadSetting(setting);
                });
            }
        }

        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        public int Order => 0;
    }
}
