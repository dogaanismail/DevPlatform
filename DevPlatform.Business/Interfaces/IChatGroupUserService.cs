using DevPlatform.Domain.Common;
using System.Threading.Tasks;

namespace DevPlatform.Business.Interfaces
{
    /// <summary>
    /// Chatgroup user interface
    /// </summary>
    public partial interface IChatGroupUserService
    {
        /// <summary>
        /// Returns chat groups by user Id.
        /// </summary>
        /// <returns></returns>
        Task<ResultModel> GetChatGroupUsers(int userId);
    }
}
