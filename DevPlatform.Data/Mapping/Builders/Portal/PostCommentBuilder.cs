using DevPlatform.Core.Domain.Portal;
using FluentMigrator.Builders.Create.Table;

namespace DevPlatform.Data.Mapping.Builders.Portal
{
    public partial class PostCommentBuilder : DevPlatformEntityBuilder<PostComment>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
               .WithColumn(nameof(PostComment.Text)).AsString(int.MaxValue).NotNullable()
               .WithColumn(nameof(PostComment.PostId)).AsInt32().NotNullable();

            //TODO ForeignKeys
        }
    }
}
