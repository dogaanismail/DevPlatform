using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPlatform.Entities.Entities
{
    public class ChatGroupUser : BaseEntity
    {
        [Required]
        [ForeignKey("ChatGroup")]
        public int ChatGroupId { get; set; }

        [Required]
        [ForeignKey("GroupMember")]
        public int MemberId { get; set; }

        public virtual AppUser GroupMember { get; set; }
        public virtual ChatGroup ChatGroup { get; set; }
    }
}
