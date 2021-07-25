using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Domain.Portal;
using DevPlatform.Data.Extensions;
using FluentMigrator.Builders.Create.Table;
using System.Data;

namespace DevPlatform.Data.Mapping.Builders.Portal
{
    public partial class PostBuilder : DevPlatformEntityBuilder<Post>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
               .WithColumn(nameof(Post.Text)).AsString(int.MaxValue).NotNullable()
               .WithColumn(nameof(Post.PostType)).AsInt32().NotNullable()
               .WithColumn(nameof(Post.CreatedBy)).AsInt32().NotNullable().WithDefaultValue(0).ForeignKey<AppUser>(onDelete: Rule.None)
               .WithColumn(nameof(Post.ModifiedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
               .WithColumn(nameof(Post.CreatedDate)).AsDateTime().NotNullable()
               .WithColumn(nameof(Post.ModifiedDate)).AsDateTime().Nullable()
               .WithColumn(nameof(Post.StatusId)).AsInt32().Nullable()
               .WithColumn(nameof(Post.Deleted)).AsBoolean().WithDefaultValue(0).NotNullable();
            #endregion
        }
    }
}
