using DevPlatform.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Domain.Story
{
    public partial class StoryComment : BaseEntity
    {
        public string Text { get; set; }

        [Required]
        [ForeignKey("CommentStory")]
        public int StoryId { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(StoryId), OtherKey = nameof(Story.Id), CanBeNull = true)]
        public virtual Story CommentStory { get; set; }
    }
}
