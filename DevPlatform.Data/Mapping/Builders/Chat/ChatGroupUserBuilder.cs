using DevPlatform.Core.Domain.Chat;
using FluentMigrator.Builders.Create.Table;

namespace DevPlatform.Data.Mapping.Builders.Chat
{
    public partial class ChatGroupUserBuilder : DevPlatformEntityBuilder<ChatGroupUser>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
              .WithColumn(nameof(ChatGroupUser.StatusId)).AsInt32().NotNullable();
        }
    }
}
