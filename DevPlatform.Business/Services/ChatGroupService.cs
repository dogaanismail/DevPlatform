using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Chat;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using DevPlatform.Repository.Generic;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// Chatgroup service
    /// </summary>
    public partial class ChatGroupService : IChatGroupService
    {
        #region Fields
        private readonly IRepository<ChatMessage> _chatRepository;
        private readonly IRepository<ChatGroup> _chatGroupRepository;
        private readonly IRepository<ChatGroupUser> _chatGroupUserRepository;
        private readonly UserManager<AppUser> _userManager;
        #endregion

        #region Ctor
        public ChatGroupService(IRepository<ChatMessage> chatRepository, IRepository<ChatGroupUser> chatGroupUserRepository,
            IRepository<ChatGroup> chatGroupRepository,
            UserManager<AppUser> userManager)
        {
            _chatRepository = chatRepository;
            _chatGroupUserRepository = chatGroupUserRepository;
            _chatGroupRepository = chatGroupRepository;
            _userManager = userManager;
        }

        #endregion

        #region Methods

        public virtual async Task<ResultModel> CreateChatGroupAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns chatgroups by user name
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public virtual async Task<List<ChatGroupDto>> GetChatGroupsAsync(string username)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns groupMmeber Details
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public virtual async Task<GroupMemberDetails> GetMemberDetailsByGroupNameAsync(string groupName, string userName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
