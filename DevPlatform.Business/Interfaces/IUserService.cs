using DevPlatform.Core.Domain.Identity;
using DevPlatform.Domain.Api;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto.UserDto;
using DevPlatform.Domain.ServiceResponseModels.UserService;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

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
        Task<AppUser> FindById(int id);

        /// <summary>
        /// Returns an user by userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<AppUser> FindByUserName(string userName);

        /// <summary>
        /// Returns an 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<AppUser> FindByEmail(string email);

        /// <summary>
        /// Creates an user
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        Task<IdentityResult> Create(AppUser appUser);

        /// <summary>
        /// Updates an user
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        Task<IdentityResult> Update(AppUser appUser);

        /// <summary>
        /// Deletes an user
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        Task<IdentityResult> Delete(AppUser appUser);

        /// <summary>
        /// Register a user and returns a service response model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ServiceResponse<RegisterResponse> Register(RegisterApiRequest model);

        /// <summary>
        /// Returns a user detail by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>Returns user's detail, such as personal informations, user posts, user albums</returns>
        Task<AppUserProfileDto> GetUserDetailByUserName(string userName);
    }
}
