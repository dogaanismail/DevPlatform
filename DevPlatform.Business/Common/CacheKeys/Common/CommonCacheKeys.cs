using DevPlatform.Core.Caching;

namespace DevPlatform.Business.Common.CacheKeys.Common
{
    public static partial class CommonCacheKeys
    {
        #region OpenWeather
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : ip address
        /// </remarks>
        public static CacheKey WeatherByIdAddressCacheKey => new("DevPlatform.weather.byipaddress.{0}");

        #endregion
    }
}
