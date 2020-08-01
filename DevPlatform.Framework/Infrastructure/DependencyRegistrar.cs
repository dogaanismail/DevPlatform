﻿using Autofac;
using DevPlatform.Core.Configuration;
using DevPlatform.Core.Infrastructure;
using DevPlatform.Core.Infrastructure.DependencyManagement;
using DevPlatform.Data;
using DevPlatform.Data.Migrations;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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
            //builder.RegisterType<NotificationService>().As<INotificationService>().InstancePerLifetimeScope();

            builder.RegisterType<ActionContextAccessor>().As<IActionContextAccessor>().InstancePerLifetimeScope();
        }

        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        public int Order => 0;
    }
}
