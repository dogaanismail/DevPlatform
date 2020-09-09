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
    public class AppUser : BaseEntity, IAppUser
    {
        [Required, Identity]
        [Key]
        public new int Id { get; set; }

        [Required]
        [ForeignKey("UserDetail")]
        public int DetailId { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(DetailId), OtherKey = nameof(AppUserDetail.Id), CanBeNull = true, Relationship = Relationship.OneToOne)]
        public virtual AppUserDetail UserDetail { get; set; }

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

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(Post.CreatedBy), CanBeNull = true)]
        public virtual IEnumerable<Post> UserPosts { get; set; }
    }
}
