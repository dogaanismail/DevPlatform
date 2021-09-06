using DevPlatform.Business.Interfaces.Identity;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public virtual async Task<JsonResult> GetUserDetailAsync(string username)
        {
            var data = await _userService.GetUserDetailByUserNameAsync(username);

            return OkResponse(data);
        }

        #endregion
    }
}
