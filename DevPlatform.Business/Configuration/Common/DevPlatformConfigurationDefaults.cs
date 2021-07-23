using DevPlatform.Core.Caching;
using DevPlatform.Core.Domain.Configuration;

namespace DevPlatform.Business.Configuration.Common
{
    /// <summary>
    /// Represents default values related to configuration services
    /// </summary>
    public static partial class DevPlatformConfigurationDefaults
    {
        #region Caching defaults

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        public static CacheKey SettingsAllAsDictionaryCacheKey => new("DevPlatform.setting.all.dictionary.", DevPlatformEntityCacheDefaults<Setting>.Prefix);

        #endregion

        /// <summary>
        /// Gets the path to file that contains app settings
        /// </summary>
        public static string AppConfigsFilePath => "App_Data/appsettings.json";
    }
}
