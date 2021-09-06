using DevPlatform.Core.Domain.Identity.Interfaces;
using DevPlatform.Core.Entities;
using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Domain.Identity
{
    public class AppUserLogin : BaseEntity, IAppUserLogin
    {
        [Required, Identity]
        [Key]
        public override int Id { get => base.Id; set => base.Id = value; }

        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
        public int UserId { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(UserId), OtherKey = nameof(AppUser.Id), CanBeNull = false)]
        public virtual AppUser UserLogin { get; set; }
    }
}
