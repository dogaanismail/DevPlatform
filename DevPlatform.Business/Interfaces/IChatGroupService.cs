﻿using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<ResultModel> CreateChatGroup();

        /// <summary>
        /// Returns chat groups by username.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ChatGroupDto>> GetChatGroups(string username);

        /// <summary>
        /// Returns group member details by group name.
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        Task<GroupMemberDetails> GetMemberDetailsByGroupName(string groupName, string userName);
    }
}
