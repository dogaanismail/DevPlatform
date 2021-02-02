using DevPlatform.Core.Domain.Portal;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;
using DevPlatform.Core.Domain.Identity;

namespace DevPlatform.Data.Mapping.Builders.Portal
{
    public partial class PostCommentBuilder : DevPlatformEntityBuilder<PostComment>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
               .WithColumn(nameof(PostComment.Text)).AsString(int.MaxValue).NotNullable()
               .WithColumn(nameof(PostComment.PostId)).AsInt32().NotNullable().ForeignKey<Post>(onDelete: Rule.Cascade)
               .WithColumn(nameof(PostComment.CreatedBy)).AsInt32().NotNullable().ForeignKey<AppUser>(onDelete: Rule.None)
               .WithColumn(nameof(PostComment.ModifiedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
               .WithColumn(nameof(PostComment.CreatedDate)).AsDateTime().NotNullable()
               .WithColumn(nameof(PostComment.ModifiedDate)).AsDateTime().Nullable()
               .WithColumn(nameof(PostComment.StatusId)).AsInt32().Nullable();
            #endregion
        }
    }
}
