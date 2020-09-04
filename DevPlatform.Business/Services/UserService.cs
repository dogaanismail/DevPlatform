using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// User service
    /// </summary>
    public partial class UserService : IUserService
    {
        #region Fields
        private readonly UserManager<AppUser> _userManager;
        #endregion

        #region Ctor
        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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

            return _userManager.FindByEmailAsync(email).Result;
        }

        /// <summary>
        /// Returns an appUser by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AppUser FindById(int id)
        {
            return _userManager.FindByIdAsync(id.ToString()).Result;
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

            return _userManager.FindByNameAsync(userName).Result;
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
