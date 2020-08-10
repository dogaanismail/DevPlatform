using DevPlatform.Core.Entities;
using DevPlatform.LinqToDB.Identity;
using LinqToDB.Mapping;
using System;
using System.ComponentModel.DataAnnotations;

namespace DevPlatform.Core.Domain.Identity
{
    public class AppRole : IdentityRole<int>, IEntity
    {
        [Required, Identity]
        [Key]
        public override int Id { get => base.Id; set => base.Id = value; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public int? StatusId { get; set; }      
    }
}
