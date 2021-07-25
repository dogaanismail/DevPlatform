using DevPlatform.Business.Interfaces;
using DevPlatform.Domain.Common;
using System;
using System.Threading.Tasks;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// Chatgorup user service
    /// </summary>
    public partial class ChatGroupUserService : IChatGroupUserService
    {
        public virtual async Task<ResultModel> GetChatGroupUsersAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
