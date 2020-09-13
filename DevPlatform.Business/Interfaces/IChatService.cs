using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using System.Collections.Generic;

namespace DevPlatform.Business.Interfaces
{
    /// <summary>
    /// Chat service interface
    /// </summary>
    public partial interface IChatService
    {
        /// <summary>
        /// Returns all chat messages in the system.
        /// </summary>
        /// <returns></returns>
        IEnumerable<MessageDto> GetAllMessages();

        /// <summary>
        /// Returns the messages by chatId.
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        MessageDto GetMessageById(int chatId);

        /// <summary>
        /// Returns the messages by chat group name.
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        IEnumerable<MessageDto> GetMessagesByGroupName(string groupName);


        /// <summary>
        /// Creates a chat message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        ResultModel Create(MessageDto message);
    }
}
