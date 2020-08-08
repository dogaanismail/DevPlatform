using DevPlatform.Core.Domain.Portal;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;
using DevPlatform.Core.Domain.Identity;

namespace DevPlatform.Data.Mapping.Builders.Portal
{
    public partial class PostImageBuilder : DevPlatformEntityBuilder<PostImage>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
              .WithColumn(nameof(PostImage.ImageUrl)).AsString(300).NotNullable()
              .WithColumn(nameof(PostImage.PostId)).AsInt32().NotNullable().ForeignKey<Post>(onDelete: Rule.Cascade)
              .WithColumn(nameof(PostImage.CreatedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
              .WithColumn(nameof(PostImage.ModifiedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
              .WithColumn(nameof(PostImage.CreatedDate)).AsDateTime().NotNullable()
              .WithColumn(nameof(PostImage.ModifiedDate)).AsDateTime().Nullable()
              .WithColumn(nameof(PostImage.StatusId)).AsInt32().Nullable();
            #endregion
        }
    }
}
