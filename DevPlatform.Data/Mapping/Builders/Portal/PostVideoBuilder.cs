using DevPlatform.Core.Domain.Portal;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;

namespace DevPlatform.Data.Mapping.Builders.Portal
{
    public partial class PostVideoBuilder : DevPlatformEntityBuilder<PostVideo>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
             .WithColumn(nameof(PostVideo.VideoUrl)).AsString(300).NotNullable()
             .WithColumn(nameof(PostVideo.PostId)).AsInt32().NotNullable().ForeignKey<Post>(onDelete: Rule.Cascade);
        }
    }
}
