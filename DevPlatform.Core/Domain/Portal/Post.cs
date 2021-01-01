using DevPlatform.Core.Entities;
using System.Collections.Generic;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Domain.Portal
{
    public partial class Post : BaseEntity
    {
        public Post()
        {
            PostImages = new HashSet<PostImage>();
            PostComments = new HashSet<PostComment>();
            PostVideos = new HashSet<PostVideo>();
        }

        public string Text { get; set; }
        public int? PostType { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(PostComment.Id), CanBeNull = true)]
        public virtual ICollection<PostComment> PostComments { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(PostImage.Id), CanBeNull = true)]
        public virtual ICollection<PostImage> PostImages { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(PostVideo.Id), CanBeNull = true)]
        public virtual ICollection<PostVideo> PostVideos { get; set; }
    }
}
