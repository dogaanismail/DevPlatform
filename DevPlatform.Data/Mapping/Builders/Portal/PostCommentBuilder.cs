using DevPlatform.Core.Domain.Portal;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;

namespace DevPlatform.Data.Mapping.Builders.Portal
{
    public partial class PostCommentBuilder : DevPlatformEntityBuilder<PostComment>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
               .WithColumn(nameof(PostComment.Text)).AsString(int.MaxValue).NotNullable()
               .WithColumn(nameof(PostComment.PostId)).AsInt32().NotNullable().ForeignKey<Post>(onDelete: Rule.Cascade);
        }
    }
}
