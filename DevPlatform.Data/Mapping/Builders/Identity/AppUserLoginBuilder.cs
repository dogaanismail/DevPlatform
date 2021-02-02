using DevPlatform.Core.Domain.Identity;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;
using FluentMigrator.SqlServer;

namespace DevPlatform.Data.Mapping.Builders.Identity
{
    public partial class AppUserLoginBuilder : DevPlatformEntityBuilder<AppUserLogin>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
              .WithColumn(nameof(AppUserLogin.Id)).AsInt32().NotNullable().PrimaryKey().Identity(1, 1)
              .WithColumn(nameof(AppUserLogin.LoginProvider)).AsString(256).NotNullable().PrimaryKey()
              .WithColumn(nameof(AppUserLogin.ProviderKey)).AsString(256).NotNullable().PrimaryKey()
              .WithColumn(nameof(AppUserLogin.ProviderDisplayName)).AsString(256).NotNullable()
              .WithColumn(nameof(AppUserLogin.UserId)).AsInt32().NotNullable().ForeignKey<AppUser>(onDelete: Rule.Cascade)
              .WithColumn(nameof(AppUserLogin.CreatedBy)).AsInt32().NotNullable().ForeignKey<AppUser>(onDelete: Rule.None)
              .WithColumn(nameof(AppUserLogin.ModifiedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
              .WithColumn(nameof(AppUserLogin.CreatedDate)).AsDateTime().NotNullable()
              .WithColumn(nameof(AppUserLogin.ModifiedDate)).AsDateTime().Nullable()
              .WithColumn(nameof(AppUserLogin.StatusId)).AsInt32().Nullable();
            #endregion
        }
    }
}
