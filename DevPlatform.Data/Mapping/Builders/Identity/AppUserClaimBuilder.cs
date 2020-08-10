using DevPlatform.Core.Domain.Identity;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;
using FluentMigrator.SqlServer;

namespace DevPlatform.Data.Mapping.Builders.Identity
{
    public partial class AppUserClaimBuilder : DevPlatformEntityBuilder<AppUserClaim>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
              .WithColumn(nameof(AppUserClaim.Id)).AsInt32().NotNullable().PrimaryKey().Identity(1, 1)
              .WithColumn(nameof(AppUserClaim.UserId)).AsInt32().NotNullable().ForeignKey<AppUser>(onDelete: Rule.Cascade)
              .WithColumn(nameof(AppUserClaim.ClaimType)).AsString(256).NotNullable()
              .WithColumn(nameof(AppUserClaim.ClaimValue)).AsString(256).NotNullable();
            #endregion
        }
    }
}
