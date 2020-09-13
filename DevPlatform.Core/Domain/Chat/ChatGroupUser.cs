using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Domain.Chat
{
    public partial class ChatGroupUser : BaseEntity
    {
        [Required]
        [ForeignKey("ChatGroup")]
        public int ChatGroupId { get; set; }

        [Required]
        [ForeignKey("GroupMember")]
        public int MemberId { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(MemberId), OtherKey = nameof(AppUser.Id), CanBeNull = true)]
        public virtual AppUser GroupMember { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(ChatGroupId), OtherKey = nameof(Chat.ChatGroup.Id), CanBeNull = true)]
        public virtual ChatGroup ChatGroup { get; set; }
    }
}
