using DevPlatform.Core.Domain.Identity.Interfaces;
using DevPlatform.Core.Entities;
using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Domain.Identity
{
    public class AppUserToken : BaseEntity, IAppUserToken
    {
        [Required, Identity]
        [Key]
        public override int Id { get => base.Id; set => base.Id = value; }
        public int UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(UserId), OtherKey = nameof(AppUser.Id), CanBeNull = false)]
        public virtual AppUser User { get; set; }
    }
}
