using DevPlatform.Core.Domain.Friendship;
using DevPlatform.Core.Domain.Identity;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;

namespace DevPlatform.Data.Mapping.Builders.Friendship
{
    public partial class FriendRequestBuilder : DevPlatformEntityBuilder<FriendRequest>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(FriendRequest.RequestMessage)).AsString(200).Nullable()
                .WithColumn(nameof(FriendRequest.FutureFriendId)).AsInt32().NotNullable().ForeignKey<AppUser>(onDelete: Rule.None)
                .WithColumn(nameof(FriendRequest.UserId)).AsInt32().NotNullable().ForeignKey<AppUser>(onDelete: Rule.None);
        }
    }
}
