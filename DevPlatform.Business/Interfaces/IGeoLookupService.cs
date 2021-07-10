﻿using DevPlatform.Domain.Dto.CommonDto;
using MaxMind.GeoIP2.Model;

namespace DevPlatform.Business.Interfaces
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
        string LookupCountryIsoCode(string ipAddress);

        /// <summary>
        /// Get country name
        /// </summary>
        /// <param name="ipAddress">IP address</param>
        /// <returns>Country name</returns>
        string LookupCountryName(string ipAddress);

        /// <summary>
        /// Get current city informations
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns>Current city informations</returns>
        City GetCurrentCityInformations(string ipAddress);

        /// <summary>
        /// Get current country informations
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns>Current country informations</returns>
        Country GetCurrentCountryInformations(string ipAddress);

        /// <summary>
        /// Get city and country informations
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns>Current city and country informations</returns>
        GeoLookupDto GetCityAndCountryInformations(string ipAddress);
    }
}