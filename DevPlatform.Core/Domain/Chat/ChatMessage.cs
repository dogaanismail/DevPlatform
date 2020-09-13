using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Domain.Chat
{
    public partial class ChatMessage : BaseEntity
    {
        [Required]
        public string Text { get; set; }

        [Required]
        [ForeignKey("Sender")]
        public int SenderId { get; set; }

        [Required]
        [ForeignKey("ChatGroup")]
        public int ChatGroupId { get; set; }

        public bool IsRead { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(SenderId), OtherKey = nameof(AppUser.Id), CanBeNull = true)]
        public virtual AppUser Sender { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(ChatGroupId), OtherKey = nameof(Chat.ChatGroup.Id), CanBeNull = true)]
        public virtual ChatGroup ChatGroup { get; set; }
    }
}
