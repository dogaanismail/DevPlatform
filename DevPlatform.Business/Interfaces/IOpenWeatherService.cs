using DevPlatform.Domain.Dto.CommonDto;

namespace DevPlatform.Business.Interfaces
{
    /// <summary>
    /// IOpenWeatherService implementations
    /// </summary>
    public partial interface IOpenWeatherService
    {
        /// <summary>
        /// Returns current weather by city
        /// </summary>
        /// <returns>Current weather</returns>
        WeatherResponseDto GetCurrentWeather();
    }
}
