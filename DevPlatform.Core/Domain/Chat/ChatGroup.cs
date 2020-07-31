using DevPlatform.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevPlatform.Core.Domain.Chat
{
    public class ChatGroup : BaseEntity
    {
        public ChatGroup()
        {
            Chats = new HashSet<ChatMessage>();
            ChatGroupMembers = new HashSet<ChatGroupUser>();
        }
        [MaxLength(40)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string GroupFlag { get; set; }

        public virtual ICollection<ChatMessage> Chats { get; set; }
        public virtual ICollection<ChatGroupUser> ChatGroupMembers { get; set; }
    }
}
