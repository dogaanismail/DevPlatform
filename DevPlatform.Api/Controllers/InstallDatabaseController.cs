using DevPlatform.Core.Configuration;
using DevPlatform.Core.Infrastructure;
using DevPlatform.Data;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DevPlatform.Api.Controllers
{
    [ApiController]
    public class InstallDatabaseController : BaseApiController
    {
        #region Fields
        private readonly IDevPlatformFileProvider _fileProvider;
        private readonly DevPlatformConfig _config;
        #endregion

        #region Ctor

        public InstallDatabaseController(IDevPlatformFileProvider fileProvider, DevPlatformConfig config)
        {
            _fileProvider = fileProvider;
            _config = config;
        }
        #endregion

        [HttpGet("install")]
        public string InstallDb()
        {
            var dataProvider = DataProviderManager.GetDataProvider(DataProviderType.SqlServer);
            var connectionString = "Data Source=DESKTOP-B2VJH8F\\SQLEXPRESS;Initial Catalog=MyReleaseDB;Integrated Security=True";
            DataSettingsManager.SaveSettings(new DataSettings
            {
                DataProvider = DataProviderType.SqlServer,
                ConnectionString = connectionString
            }, _fileProvider);

            DataSettingsManager.LoadSettings(reloadSettings: true);
            dataProvider.CreateDatabase(string.Empty);
            dataProvider.InitializeDatabase();

            return null;
        }
    }
}
