using DevPlatform.Business.Interfaces;
using DevPlatform.Core;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public virtual JsonResult GetCurrentCountryName()
        {
            var data = _geoLookupService.LookupCountryName(_webHelper.GetCurrentIpAddress());
            return OkResponse(data);
        }

        [HttpGet("currentcountryisocode")]
        [AllowAnonymous]
        public virtual JsonResult GetCurrentCountryIsoCode()
        {
            var data = _geoLookupService.LookupCountryIsoCode(_webHelper.GetCurrentIpAddress());
            return OkResponse(data);
        }

        [HttpGet("currentcountry")]
        [AllowAnonymous]
        public virtual JsonResult GetCurrentCountryInformations()
        {
            var data = _geoLookupService.GetCurrentCountryInformations(_webHelper.GetCurrentIpAddress());
            return OkResponse(data);
        }

        [HttpGet("currentcity")]
        [AllowAnonymous]
        public virtual JsonResult GetCurrentCityInformations()
        {
            var data = _geoLookupService.GetCurrentCityInformations(_webHelper.GetCurrentIpAddress());
            return OkResponse(data);
        }

        [HttpGet("cityandcountryinformations")]
        [AllowAnonymous]
        public virtual JsonResult GetCurrentCityAndCountryInformations()
        {
            var data = _geoLookupService.GetCityAndCountryInformations(_webHelper.GetCurrentIpAddress());
            return OkResponse(data);
        }

        #endregion
    }
}
