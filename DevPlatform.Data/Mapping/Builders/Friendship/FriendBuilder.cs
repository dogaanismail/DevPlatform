using DevPlatform.Core.Domain.Friendship;
using FluentMigrator.Builders.Create.Table;

namespace DevPlatform.Data.Mapping.Builders.Friendship
{
    public partial class FriendBuilder : DevPlatformEntityBuilder<Friend>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
               .WithColumn(nameof(Friend.StatusId)).AsInt32().Nullable();
        }
    }
}
