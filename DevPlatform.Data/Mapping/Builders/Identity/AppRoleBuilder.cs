using DevPlatform.Core.Domain.Identity;
using DevPlatform.Data.Extensions;
using FluentMigrator.Builders.Create.Table;
using FluentMigrator.SqlServer;
using System.Data;

namespace DevPlatform.Data.Mapping.Builders.Identity
{
    public partial class AppRoleBuilder : DevPlatformEntityBuilder<AppRole>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
                .WithColumn(nameof(AppRole.Id)).AsInt32().NotNullable().PrimaryKey().Identity(1, 1)
                .WithColumn(nameof(AppRole.Name)).AsString(128).NotNullable()
                .WithColumn(nameof(AppRole.NormalizedName)).AsString(128).NotNullable()
                .WithColumn(nameof(AppRole.ConcurrencyStamp)).AsString(256).NotNullable()
                .WithColumn(nameof(AppRole.CreatedBy)).AsInt32().NotNullable().ForeignKey<AppUser>(onDelete: Rule.None)
                .WithColumn(nameof(AppRole.ModifiedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
                .WithColumn(nameof(AppRole.CreatedDate)).AsDateTime().NotNullable()
                .WithColumn(nameof(AppRole.ModifiedDate)).AsDateTime().Nullable()
                .WithColumn(nameof(AppRole.StatusId)).AsInt32().Nullable();
            #endregion
        }
    }
}
