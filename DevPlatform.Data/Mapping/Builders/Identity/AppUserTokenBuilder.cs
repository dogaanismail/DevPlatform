using DevPlatform.Core.Domain.Identity;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;
using FluentMigrator.SqlServer;

namespace DevPlatform.Data.Mapping.Builders.Identity
{
    public partial class AppUserTokenBuilder : DevPlatformEntityBuilder<AppUserToken>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
            .WithColumn(nameof(AppUserToken.Id)).AsInt32().NotNullable().PrimaryKey().Identity(1, 1)
            .WithColumn(nameof(AppUserToken.UserId)).AsInt32().NotNullable().PrimaryKey().ForeignKey<AppUser>(onDelete: Rule.Cascade)
            .WithColumn(nameof(AppUserToken.LoginProvider)).AsString(256).NotNullable().PrimaryKey()
            .WithColumn(nameof(AppUserToken.Name)).AsString(256).NotNullable().PrimaryKey()
            .WithColumn(nameof(AppUserToken.Value)).AsString(256).NotNullable()
            .WithColumn(nameof(AppUserToken.CreatedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
            .WithColumn(nameof(AppUserToken.ModifiedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
            .WithColumn(nameof(AppUserToken.CreatedDate)).AsDateTime().NotNullable()
            .WithColumn(nameof(AppUserToken.ModifiedDate)).AsDateTime().Nullable()
            .WithColumn(nameof(AppUserToken.StatusId)).AsInt32().Nullable();
            #endregion
        }
    }
}
