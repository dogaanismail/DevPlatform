using DevPlatform.Core.Domain.Identity;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;

namespace DevPlatform.Business.Interfaces
{
    /// <summary>
    /// User detail service interface
    /// </summary>
    public interface IUserDetailService
    {
        /// <summary>
        /// Creates a user detail
        /// </summary>
        /// <param name="appUserDetail"></param>
        ResultModel Create(AppUserDetail appUserDetail);

        /// <summary>
        /// Returns a user detail by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        AppUserDetailDto GetUserDetailByUserName(string username);

        /// <summary>
        /// Updates signed user detail.
        /// </summary>
        /// <param name="detailDto"></param>
        /// <returns></returns>
        ResultModel Update(SignedUserDetailDto detailDto);
    }
}
