using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Security;
using DevPlatform.Domain.Api;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Authentication;
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
        #endregion

        #region Ctor
        public AccountController(ITokenService tokenService, SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, IUserDetailService userDetailService)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userDetailService = userDetailService;
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
        public virtual async Task<JsonResult> Register([FromBody] RegisterApiRequest model)
        {
            try
            {
                if (model.RePassword != model.Password)
                {
                    return BadResponse(ResultModel.Error("Repassword must match password"));
                }

                AppUserDetail appUserDetail = new AppUserDetail
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ProfilePhotoPath = "http://placehold.it/300x300",
                    CoverPhotoPath = "http://placehold.it/1030x360"
                };
                ResultModel resultModel = _userDetailService.Create(appUserDetail);
                if (!resultModel.Status)
                {
                    return BadResponse(resultModel);
                }

                AppUser userEntity = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    DetailId = appUserDetail.Id
                };

                IdentityResult result = await _userManager.CreateAsync(userEntity, model.Password);
                if (!result.Succeeded)
                {
                    Result.Status = false;
                    Result.Message = string.Join(",", result.Errors.Select(x => x.Description));

                    return BadResponse(Result);
                }
                return OkResponse(Result);
            }
            catch (Exception ex)
            {
                Result.Status = false;
                Result.Message = ex.ToString();
                return BadResponse(Result);
            }
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
            var result =  _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false).Result;
            var user = _userDetailService.GetUserDetailByUserName(model.UserName);
            if (result.Succeeded)
            {
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
