using DevPlatform.Core.Domain.Common;
using DevPlatform.Core.Entities;
using System.Collections.Generic;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Domain.Story
{
    public partial class Story : BaseEntity, ISoftDeletedEntity
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

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(StoryComment.StoryId), CanBeNull = true)]
        public virtual ICollection<StoryComment> StoryComments { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(StoryImage.StoryId), CanBeNull = true)]
        public virtual ICollection<StoryImage> StoryImages { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(StoryVideo.StoryId), CanBeNull = true)]
        public virtual ICollection<StoryVideo> StoryVideos { get; set; }
    }
}
