using DevPlatform.Core.Domain.Story;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;
using DevPlatform.Core.Domain.Identity;
using StoryEntity = DevPlatform.Core.Domain.Story.Story;

namespace DevPlatform.Data.Mapping.Builders.Story
{
    public partial class StoryImageBuilder : DevPlatformEntityBuilder<StoryImage>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
              .WithColumn(nameof(StoryImage.ImageUrl)).AsString(300).NotNullable()
              .WithColumn(nameof(StoryImage.StoryId)).AsInt32().NotNullable().ForeignKey<StoryEntity>(onDelete: Rule.Cascade)
              .WithColumn(nameof(StoryImage.CreatedBy)).AsInt32().NotNullable().ForeignKey<AppUser>(onDelete: Rule.None)
              .WithColumn(nameof(StoryImage.ModifiedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
              .WithColumn(nameof(StoryImage.CreatedDate)).AsDateTime().NotNullable()
              .WithColumn(nameof(StoryImage.ModifiedDate)).AsDateTime().Nullable()
              .WithColumn(nameof(StoryImage.StatusId)).AsInt32().Nullable();
            #endregion
        }
    }
}
