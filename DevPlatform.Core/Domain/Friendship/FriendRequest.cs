using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Domain.Friendship
{
    public partial class FriendRequest : BaseEntity
    {
        [Required]
        [ForeignKey("FutureUser")]
        public int FutureFriendId { get; set; }

        [Required]
        [ForeignKey("FriendUser")]
        public int UserId { get; set; }

        public bool IsAccepted { get; set; }
        public bool IsPending { get; set; }
        public bool IsRejected { get; set; }

        [MaxLength(150)]
        public string RequestMessage { get; set; }

        public DateTime SentDate { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(FutureFriendId), OtherKey = nameof(AppUser.Id), CanBeNull = true)]
        public virtual AppUser FutureUser { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(UserId), OtherKey = nameof(AppUser.Id), CanBeNull = true)]
        public virtual AppUser FriendUser { get; set; }
    }
}
