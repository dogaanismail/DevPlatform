using DevPlatform.Core.Domain.Story;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;
using DevPlatform.Core.Domain.Identity;
using StoryEntity = DevPlatform.Core.Domain.Story.Story;

namespace DevPlatform.Data.Mapping.Builders.Story
{
    public partial class StoryCommentBuilder : DevPlatformEntityBuilder<StoryComment>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
               .WithColumn(nameof(StoryComment.Text)).AsString(int.MaxValue).NotNullable()
               .WithColumn(nameof(StoryComment.StoryId)).AsInt32().NotNullable().ForeignKey<StoryEntity>(onDelete: Rule.Cascade)
               .WithColumn(nameof(StoryComment.CreatedBy)).AsInt32().NotNullable().ForeignKey<AppUser>(onDelete: Rule.None)
               .WithColumn(nameof(StoryComment.ModifiedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
               .WithColumn(nameof(StoryComment.CreatedDate)).AsDateTime().NotNullable()
               .WithColumn(nameof(StoryComment.ModifiedDate)).AsDateTime().Nullable()
               .WithColumn(nameof(StoryComment.StatusId)).AsInt32().Nullable();
            #endregion
        }
    }
}
