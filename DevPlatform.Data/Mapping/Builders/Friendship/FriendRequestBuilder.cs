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
            #region Methods
            table
                .WithColumn(nameof(FriendRequest.RequestMessage)).AsString(200).Nullable()
                .WithColumn(nameof(FriendRequest.FutureFriendId)).AsInt32().NotNullable().ForeignKey<AppUser>(onDelete: Rule.Cascade)
                .WithColumn(nameof(FriendRequest.UserId)).AsInt32().NotNullable().ForeignKey<AppUser>(onDelete: Rule.None)
                .WithColumn(nameof(FriendRequest.CreatedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
                .WithColumn(nameof(FriendRequest.ModifiedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
                .WithColumn(nameof(FriendRequest.CreatedDate)).AsDateTime().NotNullable()
                .WithColumn(nameof(FriendRequest.ModifiedDate)).AsDateTime().Nullable()
                .WithColumn(nameof(FriendRequest.StatusId)).AsInt32().Nullable();
            #endregion
        }
    }
}
