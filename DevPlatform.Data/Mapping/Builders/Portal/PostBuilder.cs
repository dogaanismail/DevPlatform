using DevPlatform.Core.Domain.Portal;
using FluentMigrator.Builders.Create.Table;

namespace DevPlatform.Data.Mapping.Builders.Portal
{
    public partial class PostBuilder : DevPlatformEntityBuilder<Post>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
               .WithColumn(nameof(Post.Text)).AsString(int.MaxValue).NotNullable()
               .WithColumn(nameof(Post.PostType)).AsInt32().NotNullable();
        }
    }
}
