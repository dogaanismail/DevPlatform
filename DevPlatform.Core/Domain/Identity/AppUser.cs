using DevPlatform.Core.Domain.Chat;
using DevPlatform.Core.Domain.Common;
using DevPlatform.Core.Domain.Identity.Interfaces;
using DevPlatform.Core.Domain.Portal;
using DevPlatform.Core.Entities;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Domain.Identity
{
    public class AppUser : BaseEntity, IAppUser, ISoftDeletedEntity
    {
        [Required, Identity]
        [Key]
        public override int Id { get => base.Id; set => base.Id = value; }

        [Required]
        [ForeignKey("UserDetail")]
        public int DetailId { get; set; }

        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public int AccessFailedCount { get; set; }
        public bool LockoutEnabled { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(DetailId), OtherKey = nameof(AppUserDetail.Id), CanBeNull = false)]
        public virtual AppUserDetail UserDetail { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(Post.CreatedBy), CanBeNull = true)]
        public virtual IEnumerable<Post> UserPosts { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(PostComment.CreatedBy), CanBeNull = true)]
        public virtual IEnumerable<PostComment> UserComments { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(PostImage.CreatedBy), CanBeNull = true)]
        public virtual IEnumerable<PostImage> UserPostImages { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(PostVideo.CreatedBy), CanBeNull = true)]
        public virtual IEnumerable<PostVideo> UserPostVideos { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(AppUserRole.UserId), CanBeNull = true)]
        public virtual IEnumerable<AppUserRole> UserRoles { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(ChatMessage.CreatedBy), CanBeNull = true)]
        public virtual IEnumerable<ChatMessage> UserMessages { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(ChatGroupUser.MemberId), CanBeNull = true)]
        public virtual IEnumerable<ChatGroup> UserChatGroups { get; set; }
    }
}
