using DevPlatform.Domain.Common;

namespace DevPlatform.Business.Interfaces
{
    /// <summary>
    /// Chatgroup user interface
    /// </summary>
    public interface IChatGroupUserService
    {
        /// <summary>
        /// Returns chat groups by user Id.
        /// </summary>
        /// <returns></returns>
        ResultModel GetChatGroupUsers(int userId);
    }
}
