using DevPlatform.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinqToDBAssociation = LinqToDB.Mapping;

namespace DevPlatform.Core.Domain.Album
{
    public partial class AlbumImage : BaseEntity
    {
        [MaxLength(200)]
        public string ImageUrl { get; set; }

        [Required]
        [ForeignKey("ImageAlbum")]
        public int AlbumId { get; set; }

        [LinqToDBAssociation.Association(ThisKey = nameof(AlbumId), OtherKey = nameof(Album.Id), CanBeNull = true)]
        public virtual Album ImageAlbum { get; set; }
    }
}
