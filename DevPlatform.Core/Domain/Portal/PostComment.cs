using DevPlatform.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Domain.Portal
{
    public partial class PostComment : BaseEntity
    {
        public string Text { get; set; }

        [Required]
        [ForeignKey("CommentPost")]
        public int PostId { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(PostId), OtherKey = nameof(Post.Id), CanBeNull = true)]
        public virtual Post CommentPost { get; set; }
    }
}
