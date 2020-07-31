using DevPlatform.Core.Domain.Friendship;
using FluentMigrator.Builders.Create.Table;

namespace DevPlatform.Data.Mapping.Builders.Friendship
{
    public partial class FriendRequestBuilder : DevPlatformEntityBuilder<FriendRequest>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(FriendRequest.RequestMessage)).AsString(200).Nullable();
        }
    }
}
