using System;
using System.Linq;
using System.Threading.Tasks;
using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Security;
using DevPlatform.Domain.Api;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using DevPlatform.Domain.Enumerations;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DevPlatform.Api.Controllers
{
    [ApiController]
    public partial class AccountController : BaseApiController
    {
        #region Fields
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserDetailService _userDetailService;
        private readonly IUserService _userService;
        private readonly ILogService _logService;
        #endregion

        #region Ctor
        public AccountController(ITokenService tokenService,
            SignInManager<AppUser> signInManager,
            IUserDetailService userDetailService,
            IUserService userService,
            ILogService logService)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userDetailService = userDetailService;
            _userService = userService;
            _logService = logService;
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
        public virtual async Task<JsonResult> RegisterAsync([FromBody] RegisterApiRequest model)
        {
            var serviceResponse = await _userService.RegisterAsync(model);

            if (serviceResponse.Warnings.Count > 0 || serviceResponse.Warnings.Any())
            {
                _ = _logService.InsertLogAsync(LogLevel.Error, $"AccountController- Register Error", JsonConvert.SerializeObject(serviceResponse));

                return BadResponse(new ResultModel
                {
                    Status = false,
                    Message = string.Join(Environment.NewLine, serviceResponse.Warnings.Select(err => string.Join(Environment.NewLine, err))),
                });
            }

            return OkResponse(Result);
        }

        /// <summary>
        /// Member Login
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public virtual async Task<JsonResult> LoginAsync([FromBody] LoginApiRequest model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            _ = _logService.InsertLogAsync(LogLevel.Information, $"AccountController- Login Response", JsonConvert.SerializeObject(result));

            if (result.Succeeded)
            {
                var user = await _userDetailService.GetUserDetailByUserNameAsync(model.UserName);

                var token = _tokenService.GenerateToken(new AppUserDto
                {
                    AppUserId = user.Id,
                    UserName = user.UserName,
                    CoverPhotoUrl = user.CoverPhotoUrl,
                    ProfilePhotoUrl = user.ProfilePhotoUrl,
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
