using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevPlatform.Entities.Entities
{
    public class PostImage : BaseEntity
    {
        [MaxLength(100)]
        public string ImageUrl { get; set; }

        [Required]
        [ForeignKey("ImagePost")]
        public int PostId { get; set; }

        public virtual Post ImagePost { get; set; }
    }
}
