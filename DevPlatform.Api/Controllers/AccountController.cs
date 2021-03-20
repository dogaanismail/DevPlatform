using System.Linq;
using System.Threading.Tasks;
using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Security;
using DevPlatform.Domain.Api;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevPlatform.Api.Controllers
{
    [ApiController]
    public partial class AccountController : BaseApiController
    {
        #region Fields
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserDetailService _userDetailService;
        private readonly IUserService _userService;
        #endregion

        #region Ctor
        public AccountController(ITokenService tokenService,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IUserDetailService userDetailService,
            IUserService userService)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userDetailService = userDetailService;
            _userService = userService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Member Register
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public virtual JsonResult Register([FromBody] RegisterApiRequest model)
        {
            var serviceResponse = _userService.Register(model);

            if (serviceResponse.Warnings.Count > 0 || serviceResponse.Warnings.Any())
                return BadResponse(new ResultModel
                {
                    Status = false,
                    Message = serviceResponse.Warnings.First()
                });

            return OkResponse(Result);
        }

        /// <summary>
        /// Member Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public virtual JsonResult Login([FromBody] LoginApiRequest model)
        {
            var result = _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false).Result;

            if (result.Succeeded)
            {
                var user = _userDetailService.GetUserDetailByUserName(model.UserName);
                var token = _tokenService.GenerateToken(new AppUserDto
                {
                    AppUserId = user.Id,
                    UserName = user.UserName,
                    CoverPhotoUrl = user.CoverPhotoUrl,
                    ProfilePhotoUrl = user.ProfilePhotoUrl,
                    UserPosts = user.UserPosts, //TODO: must be refactored.
                    RegisteredDate = user.RegisteredDate
                });
                return OkResponse(token);
            }
            else
            {
                Result.Status = false;
                Result.Message = "Username or password are wrong !";
                return BadResponse(Result);
            }
        }

        [HttpPost("logout")]
        public virtual async Task<JsonResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            Result.Status = true;
            Result.Message = "Successfully has logged out !";
            return OkResponse(Result);
        }

        #endregion
    }
}
