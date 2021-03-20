using DevPlatform.Core.Domain.Identity;
using DevPlatform.Domain.Api;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.ServiceResponseModels.UserService;
using Microsoft.AspNetCore.Identity;

namespace DevPlatform.Business.Interfaces
{
    /// <summary>
    /// User service interface
    /// </summary>
    public partial interface IUserService
    {
        /// <summary>
        /// Returns an user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AppUser FindById(int id);

        /// <summary>
        /// Returns an user by userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        AppUser FindByUserName(string userName);

        /// <summary>
        /// Returns an 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        AppUser FindByEmail(string email);

        /// <summary>
        /// Creates an user
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        IdentityResult Create(AppUser appUser);

        /// <summary>
        /// Updates an user
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        IdentityResult Update(AppUser appUser);

        /// <summary>
        /// Deletes an user
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        IdentityResult Delete(AppUser appUser);

        /// <summary>
        /// Register a user and returns a service response model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ServiceResponse<RegisterResponse> Register(RegisterApiRequest model);
    }
}
