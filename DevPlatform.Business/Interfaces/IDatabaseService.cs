using DevPlatform.Domain.Common;
using DevPlatform.Domain.ServiceResponseModels.DatabaseService;

namespace DevPlatform.Business.Interfaces
{
    /// <summary>
    /// Database service interface implementation
    /// </summary>
    public interface IDatabaseService
    {
        /// <summary>
        /// Installs a database according to giving informations
        /// </summary>
        /// <returns></returns>
        ServiceResponse<InstallResponse> InstallDatabase();
    }
}
