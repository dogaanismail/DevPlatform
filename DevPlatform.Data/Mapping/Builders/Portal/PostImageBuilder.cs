using DevPlatform.Core.Domain.Portal;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;

namespace DevPlatform.Data.Mapping.Builders.Portal
{
    public partial class PostImageBuilder : DevPlatformEntityBuilder<PostImage>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
              .WithColumn(nameof(PostImage.ImageUrl)).AsString(300).NotNullable()
              .WithColumn(nameof(PostImage.PostId)).AsInt32().NotNullable().ForeignKey<Post>(onDelete: Rule.Cascade);
        }
    }
}
