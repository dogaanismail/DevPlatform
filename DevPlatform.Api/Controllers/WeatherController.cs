using DevPlatform.Business.Interfaces.Common;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public virtual async Task<JsonResult> GetCurrentWeatherAsync()
        {
            var data = await _openWeatherService.GetCurrentWeatherAsync();

            return OkResponse(data);
        }

        #endregion
    }
}
