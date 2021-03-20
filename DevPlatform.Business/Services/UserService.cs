using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Domain.Api;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.ServiceResponseModels.UserService;
using DevPlatform.Repository.Generic;
using LinqToDB;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// User service
    /// </summary>
    public partial class UserService : ServiceExecute, IUserService
    {
        #region Fields
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<AppUser> _appUserRepository;
        private readonly IUserDetailService _userDetailService;
        #endregion

        #region Ctor
        public UserService(UserManager<AppUser> userManager,
            IRepository<AppUser> appUserRepository,
            IUserDetailService userDetailService)
        {
            _userManager = userManager;
            _appUserRepository = appUserRepository;
            _userDetailService = userDetailService;
        }
        #endregion

        /// <summary>
        /// Creates an appUser
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        public IdentityResult Create(AppUser appUser)
        {
            if (appUser == null)
                throw new ArgumentNullException(nameof(appUser));

            var result = _userManager.CreateAsync(appUser).Result;
            return result;
        }

        /// <summary>
        /// Deletes an appUser
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        public IdentityResult Delete(AppUser appUser)
        {
            if (appUser == null)
                throw new ArgumentNullException(nameof(appUser));

            IdentityResult result = _userManager.DeleteAsync(appUser).Result;
            return result;
        }

        /// <summary>
        /// Returns an appUser by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public AppUser FindByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            return _appUserRepository.Table.LoadWith(detail => detail.UserDetail)
                 .Where(x => x.Email == email).FirstOrDefault();
        }

        /// <summary>
        /// Returns an appUser by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AppUser FindById(int id)
        {
            return _appUserRepository.Table.LoadWith(detail => detail.UserDetail)
                .Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Returns an appUser by userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public AppUser FindByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            return _appUserRepository.Table.LoadWith(detail => detail.UserDetail)
                .Where(x => x.UserName == userName).FirstOrDefault();
        }


        /// <summary>
        /// Updates an appUser
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        public IdentityResult Update(AppUser appUser)
        {
            if (appUser == null)
                throw new ArgumentNullException(nameof(appUser));

            return _userManager.UpdateAsync(appUser).Result;
        }

        /// <summary>
        /// Register a user and returns a service response model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ServiceResponse<RegisterResponse> Register(RegisterApiRequest model)
        {
            if (model.RePassword != model.Password)
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
                    ProfilePhotoPath = "http://placehold.it/300x300",
                    CoverPhotoPath = "http://placehold.it/1030x360"
                };

                ResultModel resultModel = _userDetailService.Create(appUserDetail);

                if (!resultModel.Status)
                    return ServiceResponse((RegisterResponse)null, new List<string> { resultModel.Message });

                AppUser userEntity = new()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    DetailId = appUserDetail.Id
                };

                IdentityResult result = _userManager.CreateAsync(userEntity, model.Password).Result;

                if (!result.Succeeded && result.Errors != null && result.Errors.Any())
                {
                    serviceResponse.Warnings = result.Errors.Select(x => x.Description).ToList();
                    return serviceResponse;
                }

                serviceResponse.Success = true;
                serviceResponse.Data = new RegisterResponse
                {
                    Succeeded = result.Succeeded
                };

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Warnings.Add(ex.Message);
                return serviceResponse;
            }
        }

    }
}
