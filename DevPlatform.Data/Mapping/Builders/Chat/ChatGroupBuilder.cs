using DevPlatform.Core.Domain.Chat;
using DevPlatform.Core.Domain.Identity;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;

namespace DevPlatform.Data.Mapping.Builders.Chat
{
    public partial class ChatGroupBuilder : DevPlatformEntityBuilder<ChatGroup>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
               .WithColumn(nameof(ChatGroup.GroupFlag)).AsString(200).NotNullable()
               .WithColumn(nameof(ChatGroup.CreatedBy)).AsInt32().NotNullable().ForeignKey<AppUser>(onDelete: Rule.None)
               .WithColumn(nameof(ChatGroup.ModifiedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
               .WithColumn(nameof(ChatGroup.CreatedDate)).AsDateTime().NotNullable()
               .WithColumn(nameof(ChatGroup.ModifiedDate)).AsDateTime().Nullable()
               .WithColumn(nameof(ChatGroup.StatusId)).AsInt32().Nullable();
            #endregion
        }
    }
}
