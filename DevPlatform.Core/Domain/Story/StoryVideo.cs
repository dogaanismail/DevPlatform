using DevPlatform.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Domain.Story
{
    public partial class StoryVideo : BaseEntity
    {
        [MaxLength(100)]
        public string VideoUrl { get; set; }

        [Required]
        [ForeignKey("VideoStory")]
        public int StoryId { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(StoryId), OtherKey = nameof(Story.Id), CanBeNull = true)]
        public virtual Story VideoPost { get; set; }
    }
}
