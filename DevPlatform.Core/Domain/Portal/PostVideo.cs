using DevPlatform.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Domain.Portal
{
    public partial class PostVideo : BaseEntity
    {
        [MaxLength(100)]
        public string VideoUrl { get; set; }

        [Required]
        [ForeignKey("VideoPost")]
        public int PostId { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(PostId), OtherKey = nameof(Post.Id), CanBeNull = true)]
        public virtual Post VideoPost { get; set; }
    }
}
