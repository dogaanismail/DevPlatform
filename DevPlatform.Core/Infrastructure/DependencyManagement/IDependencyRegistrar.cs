using DevPlatform.Core.Configuration.Configs;
using Microsoft.Extensions.DependencyInjection;

namespace DevPlatform.Core.Infrastructure.DependencyManagement
{
    /// <summary>
    /// Dependency registrar interface
    /// </summary>
    public interface IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="services"></param>
        /// <param name="typeFinder"></param>
        /// <param name="config"></param>
        void Register(IServiceCollection services, ITypeFinder typeFinder, AppConfigs appConfigs);

        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        int Order { get; }
    }
}
