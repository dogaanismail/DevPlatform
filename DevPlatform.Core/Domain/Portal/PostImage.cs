using DevPlatform.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Domain.Portal
{
    public partial class PostImage : BaseEntity
    {
        [MaxLength(100)]
        public string ImageUrl { get; set; }

        [Required]
        [ForeignKey("ImagePost")]
        public int PostId { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(PostId), OtherKey = nameof(Post.Id), CanBeNull = true)]
        public virtual Post ImagePost { get; set; }
    }
}
