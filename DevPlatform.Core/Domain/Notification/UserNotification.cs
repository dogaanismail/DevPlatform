using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPlatform.Core.Domain.Notification
{
    public class UserNotification : BaseEntity
    {
        [MaxLength(150)]
        public string Title { get; set; }

        [MaxLength(259)]
        public string Detail { get; set; }

        [MaxLength(100)]
        public string DetailUrl { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        [ForeignKey("UserNotification")]
        public int SentTo { get; set; }

        public bool IsRead { get; set; }

        public virtual AppUser Notification { get; set; }
    }
}
