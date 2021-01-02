using DevPlatform.Core.Configuration;
using DevPlatform.Core.Infrastructure;
using DevPlatform.Data;
using DevPlatform.Domain.Common;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DevPlatform.Api.Controllers
{
    [ApiController]
    public partial class InstallDatabaseController : BaseApiController
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

        #region Methods

        [HttpGet("install")]
        public virtual JsonResult InstallDb()
        {
            var dataProvider = DataProviderManager.GetDataProvider(DataProviderType.SqlServer);
            var connectionString = "Data Source=DESKTOP-B2VJH8F\\SQLEXPRESS;Initial Catalog=DevPlatformDB;Integrated Security=True";
            DataSettingsManager.SaveSettings(new DataSettings
            {
                DataProvider = DataProviderType.SqlServer,
                ConnectionString = connectionString
            }, _fileProvider);

            DataSettingsManager.LoadSettings(reloadSettings: true);
            dataProvider.CreateDatabase(string.Empty);
            dataProvider.InitializeDatabase();

            if (DataSettingsManager.DatabaseIsInstalled)
            {
                Result.Status = true;
                Result.Message = "Datase has been installed!";
                return OkResponse(Result);                
            }
            return BadResponse(ResultModel.Error("The process can not be done !"));
        }

        #endregion
    }
}
