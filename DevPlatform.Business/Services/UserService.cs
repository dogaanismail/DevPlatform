using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Repository.Generic;
using LinqToDB;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// User service
    /// </summary>
    public partial class UserService : IUserService
    {
        #region Fields
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<AppUser> _appUserRepository;
        #endregion

        #region Ctor
        public UserService(UserManager<AppUser> userManager, IRepository<AppUser> appUserRepository)
        {
            _userManager = userManager;
            _appUserRepository = appUserRepository;
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

    }
}
