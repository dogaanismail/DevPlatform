using DevPlatform.Core.Domain.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Entities
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }

        [ForeignKey("CreatedUser")]  //By using CreatedUser integration, an owner of post,postComment,postLike that has been created, can be found easily thanks to it.
        public virtual int? CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("ModifiedUser")]  //ModifiedUser can be used in AdminUI.
        public virtual int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public int? StatusId { get; set; } //This can be Enumerations.

        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
        }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(AppUser.Id), CanBeNull = true)]
        public virtual AppUser CreatedUser { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(AppUser.Id), CanBeNull = true)]
        public virtual AppUser ModifiedUser { get; set; }
    }
}
