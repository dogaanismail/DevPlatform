using DevPlatform.Core.Domain.Common;
using DevPlatform.Core.Domain.Identity.Interfaces;
using DevPlatform.Core.Entities;
using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;

namespace DevPlatform.Core.Domain.Identity
{
    public class AppRole : BaseEntity, IAppRole, ISoftDeletedEntity
    {
        [Required, Identity]
        [Key]
        public override int Id { get => base.Id; set => base.Id = value; }

        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool Deleted { get; set; }
    }
}
