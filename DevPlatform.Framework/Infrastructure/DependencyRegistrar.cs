using Autofac;
using DevPlatform.Business.Interfaces;
using DevPlatform.Business.Services;
using DevPlatform.Core.Configuration;
using DevPlatform.Core.Infrastructure;
using DevPlatform.Core.Infrastructure.DependencyManagement;
using DevPlatform.Core.Security;
using DevPlatform.Data;
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

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IStoryService, StoryService>();
            services.AddScoped<IPostImageService, PostImageService>();
            services.AddScoped<IPostVideoService, PostVideoService>();
            services.AddScoped<IPostCommentService, PostCommentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserDetailService, UserDetailService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IChatGroupUserService, ChatGroupUserService>();
            services.AddScoped<IChatGroupService, ChatGroupService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<IImageProcessingService, ImageProcessingService>();
            services.AddScoped<IDatabaseService, DatabaseService>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

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
