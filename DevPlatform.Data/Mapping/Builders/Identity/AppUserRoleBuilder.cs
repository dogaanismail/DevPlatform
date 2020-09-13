using DevPlatform.Core.Domain.Identity;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;
using FluentMigrator.SqlServer;

namespace DevPlatform.Data.Mapping.Builders.Identity
{
    public partial class AppUserRoleBuilder : DevPlatformEntityBuilder<AppUserRole>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
            .WithColumn(nameof(AppUserRole.Id)).AsInt32().NotNullable().PrimaryKey().Identity(1, 1)
            .WithColumn(nameof(AppUserRole.UserId)).AsInt32().NotNullable().PrimaryKey().ForeignKey<AppUser>(onDelete: Rule.Cascade)
            .WithColumn(nameof(AppUserRole.RoleId)).AsInt32().NotNullable().PrimaryKey().ForeignKey<AppRole>(onDelete: Rule.Cascade)
            .WithColumn(nameof(AppUserRole.CreatedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
            .WithColumn(nameof(AppUserRole.ModifiedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
            .WithColumn(nameof(AppUserRole.CreatedDate)).AsDateTime().NotNullable()
            .WithColumn(nameof(AppUserRole.ModifiedDate)).AsDateTime().Nullable()
            .WithColumn(nameof(AppUserRole.StatusId)).AsInt32().Nullable();
            #endregion
        }
    }
}
