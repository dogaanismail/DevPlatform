﻿using DevPlatform.Core.Domain.Common;
using DevPlatform.Core.Entities;
using System.Collections.Generic;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Domain.Portal
{
    public partial class Post : BaseEntity, ISoftDeletedEntity
    {
        public Post()
        {
            PostImages = new HashSet<PostImage>();
            PostComments = new HashSet<PostComment>();
            PostVideos = new HashSet<PostVideo>();
        }

        public string Text { get; set; }
        public int? PostType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity has been deleted
        /// </summary>
        public bool Deleted { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(PostComment.PostId), CanBeNull = true)]
        public virtual ICollection<PostComment> PostComments { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(PostImage.PostId), CanBeNull = true)]
        public virtual ICollection<PostImage> PostImages { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(PostVideo.PostId), CanBeNull = true)]
        public virtual ICollection<PostVideo> PostVideos { get; set; }     
    }
}
