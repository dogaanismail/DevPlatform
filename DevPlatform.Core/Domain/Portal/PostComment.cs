using DevPlatform.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPlatform.Core.Domain.Portal
{
    public class PostComment : BaseEntity
    {
        public string Text { get; set; }

        [Required]
        [ForeignKey("CommentPost")]
        public int PostId { get; set; }

        public virtual Post CommentPost { get; set; }
    }
}
