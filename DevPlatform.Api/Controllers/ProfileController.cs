using DevPlatform.Business.Interfaces;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevPlatform.Api.Controllers
{
    [ApiController]
    public class ProfileController : BaseApiController
    {
        #region Fields
        private readonly IUserService _userService;

        #endregion

        #region Ctor

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Methods

        [HttpGet("getuserdetail")]
        [AllowAnonymous]
        public JsonResult GetUserDetail(string username)
        {
            var data = _userService.GetUserDetailByUserName(username);
            return OkResponse(data);
        }

        #endregion
    }
}
