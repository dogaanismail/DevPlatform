using DevPlatform.Business.Interfaces;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevPlatform.Api.Controllers
{
    [ApiController]
    public class WeatherController : BaseApiController
    {
        #region Fields
        private readonly IOpenWeatherService _openWeatherService;

        #endregion

        #region Ctor
        public WeatherController(IOpenWeatherService openWeatherService)
        {
            _openWeatherService = openWeatherService;
        }
        #endregion

        #region Methods

        [HttpGet("currentweather")]
        [AllowAnonymous]
        public virtual JsonResult GetCurrentWeather()
        {
            var data = _openWeatherService.GetCurrentWeather();
            return OkResponse(data);
        }

        #endregion
    }
}
