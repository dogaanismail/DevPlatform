using Autofac;
using Autofac.Builder;
using Autofac.Core;
using DevPlatform.Business.Interfaces;
using DevPlatform.Business.Services;
using DevPlatform.Core.Configuration;
using DevPlatform.Core.Infrastructure;
using DevPlatform.Core.Infrastructure.DependencyManagement;
using DevPlatform.Core.Security;
using DevPlatform.Data;
using DevPlatform.Repository.Generic;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Reflection;

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
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, DevPlatformConfig config)
        {
            //file provider
            builder.RegisterType<DevPlatformFileProvider>().As<IDevPlatformFileProvider>().InstancePerLifetimeScope();

            //data layer
            builder.RegisterType<DataProviderManager>().As<IDataProviderManager>().InstancePerDependency();
            builder.Register(context => context.Resolve<IDataProviderManager>().DataProvider).As<IDevPlatformDataProvider>().InstancePerDependency();

            //repositories
            builder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //services will be implemented here
            builder.RegisterType<TokenService>().As<ITokenService>().InstancePerLifetimeScope();
            builder.RegisterType<PostService>().As<IPostService>().InstancePerLifetimeScope();
            builder.RegisterType<StoryService>().As<IStoryService>().InstancePerLifetimeScope();
            builder.RegisterType<PostImageService>().As<IPostImageService>().InstancePerLifetimeScope();
            builder.RegisterType<PostVideoService>().As<IPostVideoService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<UserDetailService>().As<IUserDetailService>().InstancePerLifetimeScope();
            builder.RegisterType<ChatService>().As<IChatService>().InstancePerLifetimeScope();
            builder.RegisterType<ChatGroupUserService>().As<IChatGroupUserService>().InstancePerLifetimeScope();
            builder.RegisterType<ChatGroupService>().As<IChatGroupService>().InstancePerLifetimeScope();
            builder.RegisterType<ActionContextAccessor>().As<IActionContextAccessor>().InstancePerLifetimeScope();
            builder.RegisterType<SettingService>().As<ISettingService>().InstancePerLifetimeScope();
            builder.RegisterType<AlbumService>().As<IAlbumService>().InstancePerLifetimeScope();

            //register all settings
            builder.RegisterSource(new SettingsSource());
        }

        /// <summary>
        /// Setting source
        /// </summary>
        public class SettingsSource : IRegistrationSource
        {
            private static readonly MethodInfo _buildMethod =
                typeof(SettingsSource).GetMethod("BuildRegistration", BindingFlags.Static | BindingFlags.NonPublic);

            /// <summary>
            /// Registrations for
            /// </summary>
            /// <param name="service">Service</param>
            /// <param name="registrations">Registrations</param>
            /// <returns>Registrations</returns>
            public IEnumerable<IComponentRegistration> RegistrationsFor(Service service,
                Func<Service, IEnumerable<IComponentRegistration>> registrations)
            {
                var ts = service as TypedService;
                if (ts != null && typeof(ISettings).IsAssignableFrom(ts.ServiceType))
                {
                    var buildMethod = _buildMethod.MakeGenericMethod(ts.ServiceType);
                    yield return (IComponentRegistration)buildMethod.Invoke(null, null);
                }
            }

            private static IComponentRegistration BuildRegistration<TSettings>() where TSettings : ISettings, new()
            {
                return RegistrationBuilder
                    .ForDelegate((c, p) =>
                    {

                        try
                        {
                            return c.Resolve<ISettingService>().LoadSetting<TSettings>();
                        }
                        catch
                        {
                            if (DataSettingsManager.DatabaseIsInstalled)
                                throw;
                        }

                        return default;
                    })
                    .InstancePerLifetimeScope()
                    .CreateRegistration();
            }

            /// <summary>
            /// Is adapter for individual components
            /// </summary>
            public bool IsAdapterForIndividualComponents => false;
        }

        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        public int Order => 0;
    }
}
