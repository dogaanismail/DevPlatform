using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Infrastructure;
using DevPlatform.Data;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Enumerations;
using DevPlatform.Domain.ServiceResponseModels.DatabaseService;
using System;
using System.Threading.Tasks;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// Database service class implementation
    /// </summary>
    public partial class DatabaseService : ServiceExecute, IDatabaseService
    {
        #region Fields
        private readonly IDevPlatformFileProvider _fileProvider;
        private readonly ILogService _logService;
        #endregion

        #region Ctor

        public DatabaseService(IDevPlatformFileProvider fileProvider,
            ILogService logService)
        {
            _fileProvider = fileProvider;
            _logService = logService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Installs a database according to giving informations
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ServiceResponse<InstallResponse>> InstallDatabaseAsync()
        {
            var serviceResponse = new ServiceResponse<InstallResponse>
            {
                Success = false
            };

            try
            {
                var dataProvider = DataProviderManager.GetDataProvider(DataProviderType.SqlServer);
                var connectionString = "Data Source=DESKTOP-STEV1LL\\SQLEXPRESS;Initial Catalog=DevPlatformDB;Integrated Security=True";

                await DataSettingsManager.SaveSettingsAsync(new DataSettings
                {
                    DataProvider = DataProviderType.SqlServer,
                    ConnectionString = connectionString
                }, _fileProvider);

                await DataSettingsManager.LoadSettingsAsync(reloadSettings: true);
                dataProvider.CreateDatabase(string.Empty);

                dataProvider.InitializeDatabase();

                if (await DataSettingsManager.IsDatabaseInstalledAsync())
                {
                    serviceResponse.Success = true;
                    serviceResponse.ResultCode = ResultCode.Success;
                    serviceResponse.Data = new InstallResponse
                    {
                        Succeeded = true,
                        Message = "Database has been installed!"
                    };

                    return serviceResponse;
                }

                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.ResultCode = ResultCode.Exception;
                    serviceResponse.Data = new InstallResponse
                    {
                        Succeeded = false,
                        Message = "Database could not be installed!"
                    };

                    return serviceResponse;
                }
            }
            catch (Exception ex)
            {
                await _logService.InsertLogAsync(LogLevel.Error, $"DatabaseService- Create Error", ex.Message.ToString());
                serviceResponse.Success = false;
                serviceResponse.ResultCode = ResultCode.Exception;
                serviceResponse.Warnings.Add(ex.Message);
                return serviceResponse;
            }
        }

        #endregion
    }
}
