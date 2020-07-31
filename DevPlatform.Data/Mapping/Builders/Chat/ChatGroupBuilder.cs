using DevPlatform.Core.Domain.Chat;
using FluentMigrator.Builders.Create.Table;

namespace DevPlatform.Data.Mapping.Builders.Chat
{
    public partial class ChatGroupBuilder : DevPlatformEntityBuilder<ChatGroup>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
               .WithColumn(nameof(ChatGroup.GroupFlag)).AsString(200).NotNullable();
        }
    }
}
