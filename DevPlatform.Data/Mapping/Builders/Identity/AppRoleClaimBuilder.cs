using DevPlatform.Core.Domain.Identity;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;
using FluentMigrator.SqlServer;

namespace DevPlatform.Data.Mapping.Builders.Identity
{
    public partial class AppRoleClaimBuilder : DevPlatformEntityBuilder<AppRoleClaim>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
              .WithColumn(nameof(AppRoleClaim.Id)).AsInt32().NotNullable().PrimaryKey().Identity(1, 1)
              .WithColumn(nameof(AppRoleClaim.RoleId)).AsInt32().NotNullable().ForeignKey<AppRole>(onDelete: Rule.Cascade)
              .WithColumn(nameof(AppRoleClaim.ClaimType)).AsString(256).NotNullable()
              .WithColumn(nameof(AppRoleClaim.ClaimValue)).AsString(256).NotNullable()
              .WithColumn(nameof(AppRoleClaim.CreatedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
              .WithColumn(nameof(AppRoleClaim.ModifiedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
              .WithColumn(nameof(AppRoleClaim.CreatedDate)).AsDateTime().NotNullable()
              .WithColumn(nameof(AppRoleClaim.ModifiedDate)).AsDateTime().Nullable()
              .WithColumn(nameof(AppRoleClaim.StatusId)).AsInt32().Nullable();
            #endregion
        }
    }
}
