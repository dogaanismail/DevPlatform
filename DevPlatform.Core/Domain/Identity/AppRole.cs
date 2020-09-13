using DevPlatform.Core.Domain.Identity.Interfaces;
using DevPlatform.Core.Entities;
using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;

namespace DevPlatform.Core.Domain.Identity
{
    public class AppRole : BaseEntity, IAppRole
    {
        [Required, Identity]
        [Key]
        public new int Id { get; set; }

        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
