using DevPlatform.Core.Domain.Notification;
using FluentMigrator.Builders.Create.Table;

namespace DevPlatform.Data.Mapping.Builders.Notification
{
    public partial class UserNotificationBuilder : DevPlatformEntityBuilder<UserNotification>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
               .WithColumn(nameof(UserNotification.Title)).AsString(200).NotNullable()
               .WithColumn(nameof(UserNotification.Detail)).AsString(100).Nullable();
        }
    }
}
