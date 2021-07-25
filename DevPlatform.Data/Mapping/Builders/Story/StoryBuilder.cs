using DevPlatform.Core.Domain.Identity;
using StoryEntity = DevPlatform.Core.Domain.Story.Story;
using DevPlatform.Data.Extensions;
using FluentMigrator.Builders.Create.Table;
using System.Data;

namespace DevPlatform.Data.Mapping.Builders.Story
{
    public partial class StoryBuilder : DevPlatformEntityBuilder<StoryEntity>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
               .WithColumn(nameof(StoryEntity.Title)).AsString(150).NotNullable()
               .WithColumn(nameof(StoryEntity.Description)).AsString(500).NotNullable()
               .WithColumn(nameof(StoryEntity.CreatedBy)).AsInt32().NotNullable().ForeignKey<AppUser>(onDelete: Rule.None)
               .WithColumn(nameof(StoryEntity.ModifiedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
               .WithColumn(nameof(StoryEntity.CreatedDate)).AsDateTime().NotNullable()
               .WithColumn(nameof(StoryEntity.ModifiedDate)).AsDateTime().Nullable()
               .WithColumn(nameof(StoryEntity.StatusId)).AsInt32().Nullable()
               .WithColumn(nameof(StoryEntity.Deleted)).AsBoolean().WithDefaultValue(0).NotNullable();
            #endregion
        }
    }
}
