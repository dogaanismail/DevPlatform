using DevPlatform.Domain.Dto.CommonDto;
using MaxMind.GeoIP2.Model;
using System.Threading.Tasks;

namespace DevPlatform.Business.Interfaces.Common
{
    /// <summary>
    /// GEO lookup interface implementations
    /// </summary>
    public partial interface IGeoLookupService
    {
        /// <summary>
        /// Get country ISO code
        /// </summary>
        /// <param name="ipAddress">IP address</param>
        /// <returns>Country name</returns>
        Task<string> LookupCountryIsoCodeAsync(string ipAddress);

        /// <summary>
        /// Get country name
        /// </summary>
        /// <param name="ipAddress">IP address</param>
        /// <returns>Country name</returns>
        Task<string> LookupCountryNameAsync(string ipAddress);

        /// <summary>
        /// Get current city informations
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns>Current city informations</returns>
        Task<City> GetCurrentCityInformationsAsync(string ipAddress);

        /// <summary>
        /// Get current country informations
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns>Current country informations</returns>
        Task<Country> GetCurrentCountryInformationsAsync(string ipAddress);

        /// <summary>
        /// Get city and country informations
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns>Current city and country informations</returns>
        Task<GeoLookupDto> GetCityAndCountryInformationsAsync(string ipAddress);
    }
}
