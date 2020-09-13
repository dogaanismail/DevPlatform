using DevPlatform.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Domain.Chat
{
    public partial class ChatGroup : BaseEntity
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

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(Chat.ChatMessage.ChatGroupId), CanBeNull = true)]
        public virtual ICollection<ChatMessage> Chats { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(Chat.ChatGroupUser.MemberId), CanBeNull = true)]
        public virtual ICollection<ChatGroupUser> ChatGroupMembers { get; set; }
    }
}
