﻿using DevPlatform.Business.Common.CacheKeys.Common;
using DevPlatform.Business.Interfaces;
using DevPlatform.Core;
using DevPlatform.Core.Caching;
using DevPlatform.Core.Configuration.Settings.Common;
using DevPlatform.Domain.Dto.CommonDto;
using DevPlatform.Domain.Enumerations;
using RestSharp;
using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// OpenWeatherService class implementations
    /// </summary>
    public partial class OpenWeatherService : IOpenWeatherService
    {
        #region Fields
        private readonly OpenWeatherSettings _openWeatherSettings;
        private readonly IGeoLookupService _geoLookupService;
        private readonly IWebHelper _webHelper;
        private readonly ILogService _logService;
        private readonly IStaticCacheManager _staticCacheManager;

        #endregion

        #region Ctor

        public OpenWeatherService(OpenWeatherSettings openWeatherSettings,
            IGeoLookupService geoLookupService,
            IWebHelper webHelper,
            ILogService logService,
            IStaticCacheManager staticCacheManager)
        {
            _openWeatherSettings = openWeatherSettings;
            _geoLookupService = geoLookupService;
            _webHelper = webHelper;
            _logService = logService;
            _staticCacheManager = staticCacheManager;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="apiEndPoint"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private async Task<TResponse> CreateRequestAsync<TResponse>(string apiEndPoint, Method method) where TResponse : new()
        {
            Stopwatch sw = new();
            sw.Start();

            var _client = new RestClient(apiEndPoint);
            var request = new RestRequest(method)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddHeader("Content-type", "application/json");

            var response = await _client.ExecuteAsync(request);
            sw.Stop();

            if (response != null)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (response.Content != null)
                    {
                        TResponse data = JsonSerializer.Deserialize<TResponse>(response.Content);

                        if (_openWeatherSettings.EnabledLogging)
                            _ = _logService.InsertLogAsync(LogLevel.Information, $"OpenWeatherMap rest api process has finished! Millisecond: {sw.ElapsedMilliseconds}", Newtonsoft.Json.JsonConvert.SerializeObject(data));

                        return data;
                    }
                    else
                        return default;

                }
                else
                    return default;
            }

            else
                return default;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns current weather by city
        /// </summary>
        /// <returns>Current weather</returns>
        public virtual async Task<WeatherResponseDto> GetCurrentWeatherAsync()
        {
            string currentIpAddress = _webHelper.GetCurrentIpAddress();

            var key = _staticCacheManager.PrepareKeyForDefaultCache(CommonCacheKeys.WeatherByIpAddressCacheKey, currentIpAddress);

            return await _staticCacheManager.GetAsync<WeatherResponseDto>(key, async () =>
            {
                var locationInformation = await _geoLookupService.GetCityAndCountryInformationsAsync(currentIpAddress);
                var endPoint = $"{_openWeatherSettings.ApiUrl}/weather?q={locationInformation.CurrentCityName}&appid={_openWeatherSettings.ApiKey}";
                var response = await CreateRequestAsync<RootObject>(endPoint, Method.GET);

                if (response != null)
                {
                    return new WeatherResponseDto
                    {
                        CurrentCityName = locationInformation.CurrentCityName,
                        CurrentCountryName = locationInformation.CurrentCountryName,
                        FeelsLike = CalCelsius(response.main.feels_like),
                        Humidity = response.main.humidity,
                        WindSpeed = response.wind.speed,
                        Temperature = CalCelsius(response.main.temp),
                        WeatherIcon = $"https://openweathermap.org/img/wn/{response.weather[0].icon}@2x.png",
                        WeatherDescription = response.weather[0].description,
                        WeatherMain = response.weather[0].main
                    };
                }

                return new WeatherResponseDto();
            });
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns celsius temperature
        /// </summary>
        /// <param name="temp"></param>
        /// <returns>Type of celsius temperature</returns>
        private static double CalCelsius(double temp)
        {
            return Math.Floor(temp - 273.15);
        }

        /// <summary>
        /// Returns forecast by city
        /// </summary>
        /// <returns>Forecast for the city</returns>
        private async Task<object> GetForecast(string city)
        {
            var endPoint = $"{_openWeatherSettings.ApiUrl}/forecast?q={city}&appid={_openWeatherSettings.ApiKey}";
            var response = await CreateRequestAsync<object>(endPoint, Method.GET);

            return response;
        }

        #endregion
    }
}
