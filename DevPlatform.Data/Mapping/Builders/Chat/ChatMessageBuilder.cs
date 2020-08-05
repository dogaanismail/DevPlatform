using DevPlatform.Core.Domain.Chat;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;
using DevPlatform.Core.Domain.Identity;

namespace DevPlatform.Data.Mapping.Builders.Chat
{
    public partial class ChatMessageBuilder : DevPlatformEntityBuilder<ChatMessage>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
               .WithColumn(nameof(ChatMessage.Text)).AsString(500).NotNullable()
               .WithColumn(nameof(ChatMessage.IsRead)).AsBoolean().Nullable()
               .WithColumn(nameof(ChatMessage.ChatGroupId)).AsInt32().NotNullable().ForeignKey<ChatGroup>(onDelete: Rule.Cascade)
               .WithColumn(nameof(ChatMessage.SenderId)).AsInt32().NotNullable().ForeignKey<AppUser>(onDelete: Rule.Cascade);

        }
    }
}
