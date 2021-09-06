using DevPlatform.Business.Interfaces.Chat;
using DevPlatform.Core.Domain.Chat;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using DevPlatform.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevPlatform.Business.Services.Chat
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
        public virtual async Task<List<MessageDto>> GetAllMessagesAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a message by chat Id
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        public virtual async Task<MessageDto> GetMessageByIdAsync(int chatId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns messages by groupName
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public virtual async Task<List<MessageDto>> GetMessagesByGroupNameAsync(string groupName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a chat message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual async Task<ResultModel> CreateAsync (MessageDto message)
        {
            throw new NotImplementedException();
        }
    }
}
