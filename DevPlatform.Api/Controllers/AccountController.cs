using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Security;
using DevPlatform.Domain.Api;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevPlatform.Api.Controllers
{
    public class AccountController : BaseApiController
    {
        #region Ctor
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(ITokenService tokenService, SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        #endregion

        /// <summary>
        /// Member Register
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<JsonResult> Register([FromBody] RegisterApiRequest model)
        {
            try
            {
                if (model.RePassword != model.Password)
                {
                    return BadResponse(ResultModel.Error("Repassword must match password"));
                }

                AppUser userEntity = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    CreatedDate = DateTime.Now
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

        [HttpPost("logout")]
        public async Task<JsonResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            Result.Status = true;
            Result.Message = "Successfully has logged out !";
            return OkResponse(Result);
        }


    }
}
