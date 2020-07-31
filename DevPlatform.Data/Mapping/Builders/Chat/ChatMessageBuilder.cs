using DevPlatform.Core.Domain.Chat;
using FluentMigrator.Builders.Create.Table;

namespace DevPlatform.Data.Mapping.Builders.Chat
{
    public partial class ChatMessageBuilder : DevPlatformEntityBuilder<ChatMessage>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
               .WithColumn(nameof(ChatMessage.Text)).AsString(500).NotNullable();
        }
    }
}
