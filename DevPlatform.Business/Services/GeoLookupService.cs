using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Configuration.Settings.Common;
using DevPlatform.Core.Infrastructure;
using DevPlatform.Domain.Dto.CommonDto;
using DevPlatform.Domain.Enumerations;
using MaxMind.GeoIP2;
using MaxMind.GeoIP2.Exceptions;
using MaxMind.GeoIP2.Model;
using MaxMind.GeoIP2.Responses;
using System;
using System.Diagnostics;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// GEO lookup service
    /// </summary>
    public partial class GeoLookupService : IGeoLookupService
    {
        #region Fields

        private readonly ILogService _logService;
        private readonly IDevPlatformFileProvider _fileProvider;
        private readonly GeoLookupSettings _geoLookupSettings;

        #endregion

        #region Ctor

        public GeoLookupService(ILogService logService,
            IDevPlatformFileProvider fileProvider,
            GeoLookupSettings geoLookupSettings)
        {
            _logService = logService;
            _fileProvider = fileProvider;
            _geoLookupSettings = geoLookupSettings;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Get current country information by user's ip address
        /// </summary>
        /// <param name="ipAddress">IP address</param>
        /// <returns>Current country informations</returns>
        protected virtual CountryResponse GetCountryInformation(string ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress))
                return null;

            try
            {
                Stopwatch sw = new();
                sw.Start();
                var databasePath = _fileProvider.MapPath("~/App_Data/GeoLite2-Country.mmdb");
                var reader = new DatabaseReader(databasePath);
                var currentCountry = reader.Country(ipAddress);
                sw.Stop();

                if (_geoLookupSettings.EnabledLogging)
                    _logService.InsertLogAsync(LogLevel.Information, $"MaxMind.GeoIP2 current country reading process has finished! Millisecond: {sw.ElapsedMilliseconds}", ipAddress);

                return currentCountry;
            }

            catch (GeoIP2Exception)
            {
                _logService.InsertLogAsync(LogLevel.Error, $"Cannot load MaxMind record!", ipAddress);
                return null;
            }
            catch (Exception exc)
            {
                _logService.WarningAsync("Cannot load MaxMind record", exc);
                return null;
            }
        }

        /// <summary>
        /// Get current city information by user's ip address
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns>Current city informations</returns>
        protected virtual CityResponse GetCityInformation(string ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress))
                return null;

            try
            {
                Stopwatch sw = new();
                sw.Start();
                var databasePath = _fileProvider.MapPath("~/App_Data/GeoLite2-City.mmdb");
                var reader = new DatabaseReader(databasePath);
                var currentCity = reader.City(ipAddress);
                sw.Stop();

                if (_geoLookupSettings.EnabledLogging)
                    _logService.InsertLogAsync(LogLevel.Information, $"MaxMind.GeoIP2 current city reading process has finished! Millisecond: {sw.ElapsedMilliseconds}", ipAddress);

                return currentCity;
            }

            catch (GeoIP2Exception)
            {
                _logService.InsertLogAsync(LogLevel.Error, $"Cannot load MaxMind record!", ipAddress);
                return null;
            }
            catch (Exception exc)
            {
                _logService.WarningAsync("Cannot load MaxMind record", exc);
                return null;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get country ISO code
        /// </summary>
        /// <param name="ipAddress">IP address</param>
        /// <returns>Country name</returns>
        public virtual string LookupCountryIsoCode(string ipAddress)
        {
            var response = GetCountryInformation(ipAddress);
            if (response?.Country != null)
                return response.Country.IsoCode;

            return string.Empty;
        }

        /// <summary>
        /// Get country name
        /// </summary>
        /// <param name="ipAddress">IP address</param>
        /// <returns>Country name</returns>
        public virtual string LookupCountryName(string ipAddress)
        {
            var response = GetCountryInformation(ipAddress);
            if (response?.Country != null)
                return response.Country.Name;

            return string.Empty;
        }

        /// <summary>
        /// Get current city informations
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns>Current city informations</returns>
        public virtual City GetCurrentCityInformations(string ipAddress)
        {
            var response = GetCityInformation(ipAddress);
            if (response?.City != null)
                return response.City;

            return new City();
        }

        /// <summary>
        /// Get current country informations
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns>Current country informations</returns>
        public virtual Country GetCurrentCountryInformations(string ipAddress)
        {
            var response = GetCountryInformation(ipAddress);
            if (response?.Country != null)
                return response.Country;

            return new Country();
        }

        /// <summary>
        /// Get city and country informations
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns>Current city and country informations</returns>
        public GeoLookupDto GetCityAndCountryInformations(string ipAddress)
        {
            var response = GetCityInformation(ipAddress);
            if (response?.Country != null && response?.City != null)
            {
                return new GeoLookupDto
                {
                    CurrentCountryName = response.Country?.Name,
                    CurrentCityName = response.City?.Name,
                    CurrentCountryIsoCode = response.Country?.IsoCode,
                    CurrentCityIsoCode = response.MostSpecificSubdivision?.IsoCode,
                    CurrentPostalCode = response.Postal?.Code,
                    CurrentContinentName = response.Continent?.Name,
                    Latitude = response.Location?.Latitude?.ToString(),
                    Longitude = response.Location?.Longitude?.ToString(),
                    TimeZone = response.Location?.TimeZone,
                    IsInEuropeanUnion = response?.RegisteredCountry?.IsInEuropeanUnion,

                };
            }

            return new GeoLookupDto();
        }

        #endregion
    }
}
