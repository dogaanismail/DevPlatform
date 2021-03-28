using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Infrastructure;
using DevPlatform.Data;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Enumerations;
using DevPlatform.Domain.ServiceResponseModels.DatabaseService;
using System;

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
        public ServiceResponse<InstallResponse> InstallDatabase()
        {
            var serviceResponse = new ServiceResponse<InstallResponse>
            {
                Success = false
            };

            try
            {
                var dataProvider = DataProviderManager.GetDataProvider(DataProviderType.SqlServer);
                var connectionString = "Data Source=DESKTOP-STEV1LL\\SQLEXPRESS;Initial Catalog=DevPlatformDB;Integrated Security=True";

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
                    serviceResponse.Success = true;
                    serviceResponse.Data = new InstallResponse
                    {
                        Succeeded = true,
                        Message = "Database has been installed!"
                    };

                    return serviceResponse;
                }

                else
                {
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
                _logService.InsertLogAsync(LogLevel.Error, $"DatabaseService- Create Error", ex.Message.ToString());
                serviceResponse.Success = false;
                serviceResponse.Warnings.Add(ex.Message);
                return serviceResponse;
            }
        }

        #endregion
    }
}
