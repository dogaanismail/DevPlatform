using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using DevPlatform.Repository.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// User detail service
    /// </summary>
    public class UserDetailService : IUserDetailService
    {
        #region Fields
        private readonly IRepository<AppUserDetail> _appUserDetailRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        #endregion

        #region Ctor
        public UserDetailService(IRepository<AppUserDetail> appUserDetailRepository,
            IUserService userService, IPostService postService,
            UserManager<AppUser> userManager)
        {
            _appUserDetailRepository = appUserDetailRepository;
            _userService = userService;
            _postService = postService;
            _userManager = userManager;
        }
        #endregion

        /// <summary>
        /// Creates an user detail
        /// </summary>
        /// <param name="appUserDetail"></param>
        /// <returns></returns>
        public ResultModel Create(AppUserDetail appUserDetail)
        {
            if (appUserDetail == null)
                throw new ArgumentNullException(nameof(appUserDetail));

            _appUserDetailRepository.Insert(appUserDetail);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        /// <summary>
        /// Returns an user detail by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public AppUserDetailDto GetUserDetailByUserName(string userName)
        {
            //TODO: Code needs to be refactored
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            var appUser = _userManager.Users.Where(x => x.UserName == userName).Include(y => y.UserDetail).FirstOrDefaultAsync().Result;

            AppUserDetailDto postListDto = new AppUserDetailDto
            {
                Id = appUser.Id,
                UserName = appUser.UserName,
                UserPosts = _postService.GetUserPostsWithDto(appUser.Id),
                CoverPhotoUrl = appUser.UserDetail.CoverPhotoPath ?? null,
                ProfilePhotoUrl = appUser.UserDetail.ProfilePhotoPath ?? null,
                RegisteredDate = appUser.CreatedDate
            };
            return postListDto;
        }

        /// <summary>
        /// Updates an appUser detail
        /// </summary>
        /// <param name="detailDto"></param>
        /// <returns></returns>
        public ResultModel Update(SignedUserDetailDto detailDto)
        {
            //TODO: Code needs to be refactored
            if (detailDto == null)
                throw new ArgumentNullException(nameof(detailDto));

            var appUser = _userManager.Users.Where(x => x.UserName == detailDto.UserName).Include(y => y.UserDetail).FirstOrDefaultAsync().Result;
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
            _appUserDetailRepository.Update(detail);
            return new ResultModel { Status = true, Message = "Update Process Success ! " };
        }
    }
}
