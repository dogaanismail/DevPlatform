using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Domain.Friendship
{
    public partial class Friend : BaseEntity
    {
        [Required]
        [ForeignKey("FutureFriend")]
        public int FutureFriendId { get; set; }

        [Required]
        [ForeignKey("FriendUser")]
        public int UserId { get; set; }

        public DateTime BecameFriendDate { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(FutureFriendId), OtherKey = nameof(AppUser.Id), CanBeNull = true)]
        public virtual AppUser FutureFriend { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(UserId), OtherKey = nameof(AppUser.Id), CanBeNull = true)]
        public virtual AppUser FriendUser { get; set; }
    }
}
