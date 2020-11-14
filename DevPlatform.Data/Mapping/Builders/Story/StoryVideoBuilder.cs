using DevPlatform.Core.Domain.Story;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;
using DevPlatform.Core.Domain.Identity;
using StoryEntity = DevPlatform.Core.Domain.Story.Story;

namespace DevPlatform.Data.Mapping.Builders.Story
{
    public partial class StoryVideoBuilder : DevPlatformEntityBuilder<StoryVideo>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
             .WithColumn(nameof(StoryVideo.VideoUrl)).AsString(300).NotNullable()
             .WithColumn(nameof(StoryVideo.StoryId)).AsInt32().NotNullable().ForeignKey<StoryEntity>(onDelete: Rule.Cascade)
             .WithColumn(nameof(StoryVideo.CreatedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
             .WithColumn(nameof(StoryVideo.ModifiedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
             .WithColumn(nameof(StoryVideo.CreatedDate)).AsDateTime().NotNullable()
             .WithColumn(nameof(StoryVideo.ModifiedDate)).AsDateTime().Nullable()
             .WithColumn(nameof(StoryVideo.StatusId)).AsInt32().Nullable();
            #endregion
        }
    }
}
