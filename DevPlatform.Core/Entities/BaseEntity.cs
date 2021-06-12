using DevPlatform.Core.Domain.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Entities
{
    /// <summary>
    /// BaseEntity class implementation
    /// </summary>
    public class BaseEntity : IEntity
    {
        public virtual int Id { get; set; }

        /// <summary>
        /// //By using CreatedUser integration, an owner of post,postComment,postLike that has been created, can be found easily thanks to it.
        /// </summary>
        [ForeignKey("CreatedUser")]
        public virtual int? CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// //ModifiedUser can be used in AdminUI.
        /// </summary>
        [ForeignKey("ModifiedUser")]
        public virtual int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// //This can be Enumerations.
        /// </summary>
        public int? StatusId { get; set; }

        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
        }

        [LinqToDBAssociation.Association(ThisKey = nameof(CreatedBy), OtherKey = nameof(AppUser.Id), CanBeNull = false)]
        public virtual AppUser CreatedUser { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(ModifiedBy), OtherKey = nameof(AppUser.Id), CanBeNull = true)]
        public virtual AppUser ModifiedUser { get; set; }
    }
}
