using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using System.Collections.Generic;

namespace DevPlatform.Business.Interfaces
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
        ResultModel CreateChatGroup();

        /// <summary>
        /// Returns chat groups by username.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ChatGroupDto> GetChatGroups(string username);

        /// <summary>
        /// Returns group member details by group name.
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        GroupMemberDetails GetMemberDetailsByGroupName(string groupName, string userName);
    }
}
