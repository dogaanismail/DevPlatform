using DevPlatform.Core.Domain.Identity;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;

namespace DevPlatform.Data.Mapping.Builders.Identity
{
    public partial class AppRoleClaimBuilder : DevPlatformEntityBuilder<AppRoleClaim>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
              .WithColumn(nameof(AppRoleClaim.RoleId)).AsInt32().NotNullable().ForeignKey<AppRole>(onDelete: Rule.Cascade)
              .WithColumn(nameof(AppRoleClaim.ClaimType)).AsString(256).NotNullable()
              .WithColumn(nameof(AppRoleClaim.ClaimValue)).AsString(256).NotNullable();
            #endregion
        }
    }
}
