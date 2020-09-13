using DevPlatform.Core.Domain.Identity.Interfaces;
using DevPlatform.Core.Entities;
using LinqToDB.Mapping;
using System.ComponentModel.DataAnnotations;

namespace DevPlatform.Core.Domain.Identity
{
    public class AppUserRole : BaseEntity, IAppUserRole
    {
        [Required, Identity]
        [Key]
        public new int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
