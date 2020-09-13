using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Chat;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using DevPlatform.Repository.Extensions;
using DevPlatform.Repository.Generic;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public ResultModel CreateChatGroup()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns chatgroups by user name
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public IEnumerable<ChatGroupDto> GetChatGroups(string username)
        {
            //needs to have code refactoring later
            var authUser = _userManager.FindByNameAsync(username).Result;
            if (authUser != null)
            {
                //getting chatgroups 
                var chatGroups = _chatGroupRepository.GetList(null, x => x.Include(y => y.ChatGroupMembers)
                .ThenInclude(t => t.GroupMember).ThenInclude(a => a.UserDetail))
                .Where(c => c.ChatGroupMembers.Any(ty => ty.GroupMember.Id == authUser.Id))
                .ToList();

                //needs to have code refactoring because of selecting single user of a chat group
                IEnumerable<ChatGroupDto> chatGroupsDto = chatGroups.Where(x => x.ChatGroupMembers.Any(t => t.GroupMember.Id != authUser.Id)).Select(p => new ChatGroupDto
                {
                    AppUserId = p.ChatGroupMembers.Where(x => x.GroupMember.Id != authUser.Id).FirstOrDefault().GroupMember.Id, //code refactoring
                    ChatGroupId = p.Id,
                    ChatGroupName = p.Name,
                    CreatedDate = p.CreatedDate,
                    UserName = p.ChatGroupMembers.Where(x => x.GroupMember.Id != authUser.Id).FirstOrDefault().GroupMember.UserName, //code refactoring
                    ProfilePhotoUrl = p.ChatGroupMembers.Where(x => x.GroupMember.Id != authUser.Id).FirstOrDefault().GroupMember.UserDetail.ProfilePhotoPath //code refactoring
                });

                return chatGroupsDto;
            }
            return null;
        }

        /// <summary>
        /// Returns groupMmeber Details
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public GroupMemberDetails GetMemberDetailsByGroupName(string groupName, string userName)
        {
            var chatGroup = _chatGroupRepository.Find(x => x.Name == groupName,
                x => x.Include(t => t.ChatGroupMembers)
                .ThenInclude(us => us.GroupMember)
                .ThenInclude(y => y.UserDetail)).FirstOrDefault();

            var member = chatGroup.ChatGroupMembers.Where(x => x.GroupMember.UserName != userName).FirstOrDefault();
            GroupMemberDetails details = new GroupMemberDetails
            {
                MemberId = member.GroupMember.Id,
                MemberName = member.GroupMember.UserName,
                ProfilePhotoUrl = member.GroupMember.UserDetail.ProfilePhotoPath
            };

            return details;
        }
    }
}
