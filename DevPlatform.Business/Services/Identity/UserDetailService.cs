using DevPlatform.Business.Common.CacheKeys.Identity;
using DevPlatform.Business.Interfaces.Identity;
using DevPlatform.Core.Caching;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using DevPlatform.Repository.Generic;
using LinqToDB;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevPlatform.Business.Services.Identity
{
    /// <summary>
    /// User detail service
    /// </summary>
    public partial class UserDetailService : IUserDetailService
    {
        #region Fields
        private readonly IRepository<AppUserDetail> _appUserDetailRepository;
        private readonly IRepository<AppUser> _appUserRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IStaticCacheManager _staticCacheManager;

        #endregion

        #region Ctor
        public UserDetailService(IRepository<AppUserDetail> appUserDetailRepository,
            UserManager<AppUser> userManager,
            IRepository<AppUser> appUserRepository,
            IStaticCacheManager staticCacheManager)
        {
            _appUserDetailRepository = appUserDetailRepository;
            _userManager = userManager;
            _appUserRepository = appUserRepository;
            _staticCacheManager = staticCacheManager;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Creates an user detail
        /// </summary>
        /// <param name="appUserDetail"></param>
        /// <returns></returns>
        public virtual async Task<ResultModel> CreateAsync(AppUserDetail appUserDetail)
        {
            if (appUserDetail == null)
                throw new ArgumentNullException(nameof(appUserDetail));

            await _appUserDetailRepository.InsertAsync(appUserDetail);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        /// <summary>
        /// Returns an user detail by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual async Task<AppUserDetailDto> GetUserDetailByUserNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return null;

            var cacheKey = _staticCacheManager.PrepareKeyForDefaultCache(AppUserCacheKeys.UserDetailByUserNameCacheKey, userName);

            return await _staticCacheManager.GetAsync(cacheKey, async () =>
            {
                return await _appUserRepository.Table.Where(x => x.UserName == userName)
                .Select(user => new AppUserDetailDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    CoverPhotoUrl = user.UserDetail.CoverPhotoPath,
                    ProfilePhotoUrl = user.UserDetail.ProfilePhotoPath,
                    RegisteredDate = user.CreatedDate,
                }).FirstOrDefaultAsyncMethod();
            });
        }

        /// <summary>
        /// Updates an appUser detail
        /// </summary>
        /// <param name="detailDto"></param>
        /// <returns></returns>
        public virtual async Task<ResultModel> UpdateAsync(SignedUserDetailDto detailDto)
        {
            //TODO: Code needs to be refactored
            if (detailDto == null)
                throw new ArgumentNullException(nameof(detailDto));

            var appUser = _userManager.Users.Where(x => x.UserName == detailDto.UserName).LoadWith(y => y.UserDetail).FirstOrDefault();
            var detail = appUser.UserDetail;

            detail.FirstName = detailDto.FirstName;
            detail.LastName = detailDto.LastName;
            detail.BirthDate = detailDto.BirthDate;
            detail.City = detailDto.City;
            detail.Country = detailDto.Country;
            detail.AboutMe = detailDto.AboutMe;
            detail.UniversityName = detailDto.UniversityName;
            detail.UniStartDate = detailDto.StartDate;
            detail.UniFinishUpDate = detailDto.FinishUpDate;
            detail.HasGraduated = detailDto.HasGraduated;
            detail.UniversityDesc = detailDto.UniversityDesc;
            detail.CompanyName = detailDto.CompanyName;
            detail.Designation = detailDto.Designation;
            detail.ModifiedDate = DateTime.Now;
            await _appUserDetailRepository.UpdateAsync(detail);
            return new ResultModel { Status = true, Message = "Update Process Success ! " };
        }

        #endregion
    }
}
