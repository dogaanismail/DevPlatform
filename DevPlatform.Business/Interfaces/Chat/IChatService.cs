using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevPlatform.Business.Interfaces.Chat
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
        Task<List<MessageDto>> GetAllMessagesAsync();

        /// <summary>
        /// Returns the messages by chatId.
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        Task<MessageDto> GetMessageByIdAsync(int chatId);

        /// <summary>
        /// Returns the messages by chat group name.
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
        Task<List<MessageDto>> GetMessagesByGroupNameAsync(string groupName);


        /// <summary>
        /// Creates a chat message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<ResultModel> CreateAsync(MessageDto message);
    }
}
