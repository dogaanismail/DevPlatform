using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevPlatform.Business.Interfaces.Chat
{
    /// <summary>
    /// Chatgroup interface
    /// </summary>
    public partial interface IChatGroupService
    {
        /// <summary>
        /// Creates a chat group.
        /// </summary>
        /// <returns></returns>
        Task<ResultModel> CreateChatGroupAsync();

        /// <summary>
        /// Returns chat groups by username.
        /// </summary>
        /// <returns></returns>
        Task<List<ChatGroupDto>> GetChatGroupsAsync(string username);

        /// <summary>
        /// Returns group member details by group name.
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        Task<GroupMemberDetails> GetMemberDetailsByGroupNameAsync(string groupName, string userName);
    }
}
