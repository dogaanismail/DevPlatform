using DevPlatform.Core;
using DevPlatform.Core.Configuration.Configs;
using DevPlatform.Core.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DevPlatform.Business.Configuration.Common
{
    /// <summary>
    /// Represents the app settings helper
    /// </summary>
    public partial class AppConfigsHelper
    {
        #region Methods

        /// <summary>
        /// Save app settings to the file
        /// </summary>
        /// <param name="appSettings">App settings</param>
        /// <param name="fileProvider">File provider</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public static async Task SaveAppSettingsAsync(AppConfigs appSettings, IDevPlatformFileProvider fileProvider = null)
        {
            Singleton<AppConfigs>.Instance = appSettings ?? throw new ArgumentNullException(nameof(appSettings));

            fileProvider ??= CommonHelper.DefaultFileProvider;

            //create file if not exists
            var filePath = fileProvider.MapPath(DevPlatformConfigurationDefaults.AppConfigsFilePath);
            fileProvider.CreateFile(filePath);

            //check additional configuration parameters
            var additionalData = JsonConvert.DeserializeObject<AppConfigs>(await fileProvider.ReadAllTextAsync(filePath, Encoding.UTF8))?.AdditionalData;
            appSettings.AdditionalData = additionalData;

            //save app settings to the file
            var text = JsonConvert.SerializeObject(appSettings, Formatting.Indented);
            await fileProvider.WriteAllTextAsync(filePath, text, Encoding.UTF8);
        }

        /// <summary>
        /// Save app settings to the file
        /// </summary>
        /// <param name="appSettings">App settings</param>
        /// <param name="fileProvider">File provider</param>
        public static void SaveAppSettings(AppConfigs appSettings, IDevPlatformFileProvider fileProvider = null)
        {
            Singleton<AppConfigs>.Instance = appSettings ?? throw new ArgumentNullException(nameof(appSettings));

            fileProvider ??= CommonHelper.DefaultFileProvider;

            //create file if not exists
            var filePath = fileProvider.MapPath(DevPlatformConfigurationDefaults.AppConfigsFilePath);
            fileProvider.CreateFile(filePath);

            //check additional configuration parameters
            var additionalData = JsonConvert.DeserializeObject<AppConfigs>(fileProvider.ReadAllText(filePath, Encoding.UTF8))?.AdditionalData;
            appSettings.AdditionalData = additionalData;

            //save app settings to the file
            var text = JsonConvert.SerializeObject(appSettings, Formatting.Indented);
            fileProvider.WriteAllText(filePath, text, Encoding.UTF8);
        }

        #endregion
    }
}
