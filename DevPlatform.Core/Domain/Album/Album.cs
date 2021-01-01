using DevPlatform.Core.Entities;
using System;
using System.Collections.Generic;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Domain.Album
{
    public partial class Album : BaseEntity
    {
        public Album()
        {
            AlbumImages = new HashSet<AlbumImage>();
        }

        public string Name { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public string Tag { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(Id), OtherKey = nameof(AlbumImage.Id), CanBeNull = true)]
        public virtual ICollection<AlbumImage> AlbumImages { get; set; }
    }
}
