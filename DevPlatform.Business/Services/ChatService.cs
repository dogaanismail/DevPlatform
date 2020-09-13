using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Chat;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using DevPlatform.Repository.Extensions;
using DevPlatform.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// Chat service interface
    /// </summary>
    public partial class ChatService : IChatService
    {
        #region Fields
        private readonly IRepository<ChatMessage> _chatRepository;
        private readonly IRepository<ChatGroupUser> _chatGroupUserRepository;
        private readonly IRepository<ChatGroup> _chatGroup;
        #endregion

        #region Ctor
        public ChatService(IRepository<ChatMessage> chatRepository,
            IRepository<ChatGroupUser> chatGroupRepository,
            IRepository<ChatGroup> chatGroup)
        {
            _chatRepository = chatRepository;
            _chatGroupUserRepository = chatGroupRepository;
            _chatGroup = chatGroup;
        }
        #endregion

        /// <summary>
        /// Returns all messages
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MessageDto> GetAllMessages()
        {
            IEnumerable<MessageDto> data = _chatRepository.Find(null, x => x.Include(u => u.Sender).
            Include(r => r.ChatGroup)).ToList().Select(x => new MessageDto
            {
                CreateDate = x.CreatedDate,
                IsRead = x.IsRead,
                SenderName = x.Sender.UserName,
                Text = x.Text,
                GroupName = x.ChatGroup.Name
            }).AsEnumerable();

            return data;
        }

        /// <summary>
        /// Returns a message by chat Id
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public MessageDto GetMessageById(int chatId)
        {
            var chat = _chatRepository.GetById(chatId, x => x.Include(y => y.Sender));

            MessageDto messageDto = new MessageDto
            {
                SenderName = chat.Sender.UserName,
                CreateDate = chat.CreatedDate,
                IsRead = chat.IsRead,
                Text = chat.Text
            };
            return messageDto;
        }

        /// <summary>
        /// Returns messages by groupName
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public IEnumerable<MessageDto> GetMessagesByGroupName(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
                throw new ArgumentNullException(nameof(groupName));

            var chatGroup = _chatGroup.Find(x => x.Name == groupName).FirstOrDefault();

            IEnumerable<MessageDto> data = _chatRepository.Find(x => x.ChatGroupId == chatGroup.Id,
                x => x.Include(u => u.Sender).ThenInclude(t => t.UserDetail)
            .Include(r => r.ChatGroup).ThenInclude(tt => tt.CreatedUser)).ToList().Select(x => new MessageDto
            {
                Text = x.Text,
                CreateDate = x.CreatedDate,
                IsRead = x.IsRead,
                SenderName = x.Sender.UserName,
                SenderId = x.Sender.Id,
                ProfilePhotoUrl = x.Sender.UserDetail.ProfilePhotoPath,
                GroupName = x.ChatGroup.Name
            }).AsEnumerable();

            return data;
        }

        /// <summary>
        /// Returns a chat message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ResultModel Create(MessageDto message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var chatGroup = _chatGroup.Find(x => x.Name == message.GroupName).FirstOrDefault();
            ChatMessage newChat = new ChatMessage
            {
                Text = message.Text,
                SenderId = message.SenderId,
                IsRead = message.IsRead,
                ChatGroupId = chatGroup.Id,
            };
            _chatRepository.Insert(newChat);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }
    }
}
