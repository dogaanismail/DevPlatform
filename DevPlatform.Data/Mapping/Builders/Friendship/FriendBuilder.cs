using DevPlatform.Core.Domain.Friendship;
using DevPlatform.Core.Domain.Identity;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;

namespace DevPlatform.Data.Mapping.Builders.Friendship
{
    public partial class FriendBuilder : DevPlatformEntityBuilder<Friend>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
               .WithColumn(nameof(Friend.StatusId)).AsInt32().Nullable()
               .WithColumn(nameof(Friend.FutureFriendId)).AsInt32().NotNullable().ForeignKey<AppUser>(onDelete: Rule.None)
               .WithColumn(nameof(Friend.UserId)).AsInt32().NotNullable().ForeignKey<AppUser>(onDelete: Rule.None);
        }
    }
}
