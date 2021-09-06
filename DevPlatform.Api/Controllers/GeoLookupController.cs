using DevPlatform.Business.Interfaces.Common;
using DevPlatform.Core;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevPlatform.Api.Controllers
{
    [ApiController]
    public partial class GeoLookupController : BaseApiController
    {
        #region Fields
        private readonly IGeoLookupService _geoLookupService;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor
        public GeoLookupController(IGeoLookupService geoLookupService,
            IWebHelper webHelper)
        {
            _webHelper = webHelper;
            _geoLookupService = geoLookupService;
        }

        #endregion

        #region Methods

        [HttpGet("currentcountryname")]
        [AllowAnonymous]
        public virtual async Task<JsonResult> GetCurrentCountryNameAsync()
        {
            var data = await _geoLookupService.LookupCountryNameAsync(_webHelper.GetCurrentIpAddress());

            return OkResponse(data);
        }

        [HttpGet("currentcountryisocode")]
        [AllowAnonymous]
        public virtual async Task<JsonResult> GetCurrentCountryIsoCodeAsync()
        {
            var data = await _geoLookupService.LookupCountryIsoCodeAsync(_webHelper.GetCurrentIpAddress());

            return OkResponse(data);
        }

        [HttpGet("currentcountry")]
        [AllowAnonymous]
        public virtual async Task<JsonResult> GetCurrentCountryInformationsAsync()
        {
            var data = await _geoLookupService.GetCurrentCountryInformationsAsync(_webHelper.GetCurrentIpAddress());

            return OkResponse(data);
        }

        [HttpGet("currentcity")]
        [AllowAnonymous]
        public virtual async Task<JsonResult> GetCurrentCityInformationsAsync()
        {
            var data = await _geoLookupService.GetCurrentCityInformationsAsync(_webHelper.GetCurrentIpAddress());

            return OkResponse(data);
        }

        [HttpGet("cityandcountryinformations")]
        [AllowAnonymous]
        public virtual async Task<JsonResult> GetCurrentCityAndCountryInformationsAsync()
        {
            var data = await _geoLookupService.GetCityAndCountryInformationsAsync(_webHelper.GetCurrentIpAddress());

            return OkResponse(data);
        }

        #endregion
    }
}
