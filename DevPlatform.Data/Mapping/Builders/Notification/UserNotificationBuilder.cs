using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Domain.Notification;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;

namespace DevPlatform.Data.Mapping.Builders.Notification
{
    public partial class UserNotificationBuilder : DevPlatformEntityBuilder<UserNotification>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
               .WithColumn(nameof(UserNotification.Title)).AsString(200).NotNullable()
               .WithColumn(nameof(UserNotification.Detail)).AsString(200).Nullable()
               .WithColumn(nameof(UserNotification.DetailUrl)).AsString(200).Nullable()
               .WithColumn(nameof(UserNotification.SentTo)).AsInt32().ForeignKey<AppUser>(onDelete: Rule.Cascade);
            #endregion
        }
    }
}
