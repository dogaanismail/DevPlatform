using DevPlatform.Data.Extensions;
using AlbumEntity = DevPlatform.Core.Domain.Album.Album;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Core.Domain.Identity;
using System.Data;

namespace DevPlatform.Data.Mapping.Builders.Album
{
    public partial class AlbumBuilder : DevPlatformEntityBuilder<AlbumEntity>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
               .WithColumn(nameof(AlbumEntity.Name)).AsString(128).NotNullable()
               .WithColumn(nameof(AlbumEntity.Place)).AsString(64).NotNullable()
               .WithColumn(nameof(AlbumEntity.Date)).AsDateTime().Nullable()
               .WithColumn(nameof(AlbumEntity.Tag)).AsString(256).NotNullable()
               .WithColumn(nameof(AlbumEntity.CreatedBy)).AsInt32().NotNullable().ForeignKey<AppUser>(onDelete: Rule.None)
               .WithColumn(nameof(AlbumEntity.ModifiedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
               .WithColumn(nameof(AlbumEntity.CreatedDate)).AsDateTime().NotNullable()
               .WithColumn(nameof(AlbumEntity.ModifiedDate)).AsDateTime().Nullable()
               .WithColumn(nameof(AlbumEntity.StatusId)).AsInt32().Nullable();
            #endregion
        }
    }
}
