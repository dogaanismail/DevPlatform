using DevPlatform.Core.Domain.Chat;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Data.Extensions;
using FluentMigrator.Builders.Create.Table;
using System.Data;

namespace DevPlatform.Data.Mapping.Builders.Chat
{
    public partial class ChatGroupUserBuilder : DevPlatformEntityBuilder<ChatGroupUser>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
              .WithColumn(nameof(ChatGroupUser.StatusId)).AsInt32().NotNullable()
              .WithColumn(nameof(ChatGroupUser.ChatGroupId)).AsInt32().NotNullable().ForeignKey<ChatGroup>(onDelete: Rule.Cascade)
              .WithColumn(nameof(ChatGroupUser.MemberId)).AsInt32().NotNullable().ForeignKey<AppUser>(onDelete: Rule.Cascade);
        }
    }
}
