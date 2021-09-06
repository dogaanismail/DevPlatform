using DevPlatform.Core.Domain.Identity;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using System.Threading.Tasks;

namespace DevPlatform.Business.Interfaces.Identity
{
    /// <summary>
    /// User detail service interface
    /// </summary>
    public partial interface IUserDetailService
    {
        /// <summary>
        /// Creates a user detail
        /// </summary>
        /// <param name="appUserDetail"></param>
        Task<ResultModel> CreateAsync(AppUserDetail appUserDetail);

        /// <summary>
        /// Returns a user detail by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<AppUserDetailDto> GetUserDetailByUserNameAsync(string username);

        /// <summary>
        /// Updates signed user detail.
        /// </summary>
        /// <param name="detailDto"></param>
        /// <returns></returns>
        Task<ResultModel> UpdateAsync(SignedUserDetailDto detailDto);
    }
}
