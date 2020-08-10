using DevPlatform.Core.Entities;
using DevPlatform.LinqToDB.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPlatform.Core.Domain.Identity
{
    public class AppUser : IdentityUser<int>, IEntity
    {
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override int Id { get => base.Id; set => base.Id = value; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public int? StatusId { get; set; }

        [Required]
        [ForeignKey("UserDetail")]
        public int DetailId { get; set; }

        public virtual AppUserDetail UserDetail { get; set; }
    }
}
