using DevPlatform.Core.Domain.Identity.Interfaces;
using DevPlatform.Core.Entities;
using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Domain.Identity
{
    public class AppUserClaim : BaseEntity, IAppUserClaim
    {
        [Required, Identity]
        [Key]
        public override int Id { get => base.Id; set => base.Id = value; }
        public int UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        /// <summary>
        /// Reads the type and value from the Claim.
        /// </summary>
        /// <param name="other"></param>
        public void InitializeFromClaim(Claim other)
        {
            ClaimType = other.Type;
            ClaimValue = other.Value;
        }

        /// <summary>
        /// Converts the entity into a Claim instance.
        /// </summary>
        /// <returns></returns>
        public Claim ToClaim()
        {
            return new Claim(ClaimType, ClaimValue);
        }

        [LinqToDBAssociation.Association(ThisKey = nameof(UserId), OtherKey = nameof(AppUser.Id), CanBeNull = false)]
        public virtual AppUser UserClaim { get; set; }
    }
}
