using DevPlatform.Business.Common.CacheKeys.Identity;
using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Caching;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Infrastructure;
using DevPlatform.Domain.Api;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto.UserDto;
using DevPlatform.Domain.Enumerations;
using DevPlatform.Domain.ServiceResponseModels.UserService;
using DevPlatform.Repository.Generic;
using LinqToDB;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// User service implementations
    /// </summary>
    public partial class UserService : ServiceExecute, IUserService
    {
        #region Fields
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<AppUser> _appUserRepository;
        private readonly IRepository<AppUserDetail> _appUserDetailRepository;
        private readonly ILogService _logService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IUserDetailService _userDetailService;

        #endregion

        #region Ctor
        public UserService(UserManager<AppUser> userManager,
            IRepository<AppUser> appUserRepository,
            IRepository<AppUserDetail> appUserDetailRepository,
            ILogService logService,
            IStaticCacheManager staticCacheManager,
            IUserDetailService userDetailService)
        {
            _userManager = userManager;
            _appUserRepository = appUserRepository;
            _appUserDetailRepository = appUserDetailRepository;
            _logService = logService;
            _staticCacheManager = staticCacheManager;
            _userDetailService = userDetailService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Creates an appUser
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        public virtual async Task<IdentityResult> CreateAsync(AppUser appUser)
        {
            if (appUser == null)
                throw new ArgumentNullException(nameof(appUser));

           return await _userManager.CreateAsync(appUser);
        }

        /// <summary>
        /// Deletes an appUser
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        public virtual async Task<IdentityResult> DeleteAsync(AppUser appUser)
        {
            if (appUser == null)
                throw new ArgumentNullException(nameof(appUser));

            return await _userManager.DeleteAsync(appUser);
        }

        /// <summary>
        /// Returns an appUser by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public virtual async Task<AppUser> FindByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(AppUserCacheKeys.UserByEmailCacheKey, email);

            return await _staticCacheManager.GetAsync(cacheKey, async () =>
            {
                return await _appUserRepository.Table.Where(x => x.Email == email).LoadWith(detail => detail.UserDetail).FirstOrDefaultAsyncMethod();
            });
        }

        /// <summary>
        /// Returns an appUser by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<AppUser> FindByIdAsync(int id)
        {
            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(DevPlatformEntityCacheDefaults<AppUser>.ByIdCacheKey, id);

            return await _staticCacheManager.GetAsync(cacheKey, async () =>
            {
                return await _appUserRepository.Table.Where(x => x.Id == id).LoadWith(detail => detail.UserDetail).FirstOrDefaultAsyncMethod();

            });
        }

        /// <summary>
        /// Returns an appUser by userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual async Task<AppUser> FindByUserNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(AppUserCacheKeys.UserByUserNameCacheKey, userName);

            return await _staticCacheManager.GetAsync(cacheKey, async () =>
            {
                return await _appUserRepository.Table.Where(x => x.UserName == userName).LoadWith(detail => detail.UserDetail).FirstOrDefaultAsyncMethod();
            });
        }

        /// <summary>
        /// Updates an appUser
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        public virtual async Task<IdentityResult> UpdateAsync(AppUser appUser)
        {
            if (appUser == null)
                throw new ArgumentNullException(nameof(appUser));

            return await _userManager.UpdateAsync(appUser);
        }

        /// <summary>
        /// Register a user and returns a service response model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task<ServiceResponse<RegisterResponse>> RegisterAsync(RegisterApiRequest model)
        {
            if (!model.RePassword.Equals(model.Password))
                return ServiceResponse((RegisterResponse)null, new List<string> { "Repassword must match password!" });

            var serviceResponse = new ServiceResponse<RegisterResponse>
            {
                Success = false
            };

            try
            {
                AppUserDetail appUserDetail = new()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ProfilePhotoPath = "https://via.placeholder.com/300x300",
                    CoverPhotoPath = "https://via.placeholder.com/1030x360"
                };

                ResultModel resultModel = await CreateUserDetailAsync(appUserDetail);

                if (!resultModel.Status)
                    return ServiceResponse((RegisterResponse)null, new List<string> { resultModel.Message });

                AppUser userEntity = new()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    DetailId = appUserDetail.Id
                };

                IdentityResult result = await _userManager.CreateAsync(userEntity, model.Password);

                if (!result.Succeeded && result.Errors != null && result.Errors.Any())
                {
                    _ = _logService.InsertLogAsync(LogLevel.Error, $"UserService- Register Error", JsonConvert.SerializeObject(result));

                    serviceResponse.Warnings = result.Errors.Select(x => x.Description).ToList();
                    return serviceResponse;
                }

                serviceResponse.Success = true;
                serviceResponse.ResultCode = ResultCode.Success;
                serviceResponse.Data = new RegisterResponse
                {
                    Succeeded = result.Succeeded
                };

                return serviceResponse;
            }
            catch (Exception ex)
            {
                await _logService.InsertLogAsync(LogLevel.Error, $"UserService- Register Exception Error: model {JsonConvert.SerializeObject(model)}", ex.Message.ToString());
                serviceResponse.Success = false;
                serviceResponse.ResultCode = ResultCode.Exception;
                serviceResponse.Warnings.Add(ex.Message);
                return serviceResponse;
            }
        }

        /// <summary>
        /// Returns a user detail by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>Returns user's detail, such as personal informations, user posts, user albums</returns>
        public virtual async Task<AppUserProfileDto> GetUserDetailByUserNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            var postService = EngineContext.Current.Resolve<IPostService>();

            var detailDto = new AppUserProfileDto
            {
                AppUserDetail = await _userDetailService.GetUserDetailByUserNameAsync(userName)
            };

            detailDto.UserPosts = await postService.GetUserPostsWithDtoAsync(detailDto.AppUserDetail.Id);

            return detailDto;
        }

        #region Private Methods

        /// <summary>
        /// Creates an user detail
        /// </summary>
        /// <param name="appUserDetail"></param>
        /// <returns></returns>
        public virtual async Task<ResultModel> CreateUserDetailAsync(AppUserDetail appUserDetail)
        {
            if (appUserDetail == null)
                throw new ArgumentNullException(nameof(appUserDetail));

            await _appUserDetailRepository.InsertAsync(appUserDetail);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        #endregion

        #endregion
    }
}
