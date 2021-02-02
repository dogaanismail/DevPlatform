using DevPlatform.Core.Domain.Album;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;
using DevPlatform.Core.Domain.Identity;
using AlbumEntity = DevPlatform.Core.Domain.Album.Album;

namespace DevPlatform.Data.Mapping.Builders.Album
{
    public class AlbumImageBuilder : DevPlatformEntityBuilder<AlbumImage>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
              .WithColumn(nameof(AlbumImage.ImageUrl)).AsString(300).NotNullable()
              .WithColumn(nameof(AlbumImage.AlbumId)).AsInt32().NotNullable().ForeignKey<AlbumEntity>(onDelete: Rule.Cascade)
              .WithColumn(nameof(AlbumImage.CreatedBy)).AsInt32().NotNullable().ForeignKey<AppUser>(onDelete: Rule.None)
              .WithColumn(nameof(AlbumImage.ModifiedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
              .WithColumn(nameof(AlbumImage.CreatedDate)).AsDateTime().NotNullable()
              .WithColumn(nameof(AlbumImage.ModifiedDate)).AsDateTime().Nullable()
              .WithColumn(nameof(AlbumImage.StatusId)).AsInt32().Nullable();
            #endregion
        }
    }
}
