using DevPlatform.Core.Entities;
using System.Collections.Generic;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Domain.Story
{
    public partial class Story : BaseEntity
    {
        public Story()
        {
            StoryImages = new HashSet<StoryImage>();
            StoryComments = new HashSet<StoryComment>();
            StoryVideos = new HashSet<StoryVideo>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public int? StoryType { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(StoryComment.Id), CanBeNull = true)]
        public virtual ICollection<StoryComment> StoryComments { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(StoryImage.Id), CanBeNull = true)]
        public virtual ICollection<StoryImage> StoryImages { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(StoryVideo.Id), CanBeNull = true)]
        public virtual ICollection<StoryVideo> StoryVideos { get; set; }
    }
}
