using DevPlatform.Core.Entities;
using DevPlatform.LinqToDB.Identity;
using LinqToDB.Mapping;
using System;
using System.ComponentModel.DataAnnotations;

namespace DevPlatform.Core.Domain.Identity
{
    public class AppUserRole : IdentityUserRole<int>, IEntity
    {
        [Required, Identity]
        [Key]
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public int? StatusId { get; set; }
    }
}
