using DevPlatform.Core.Domain.Identity;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;

namespace DevPlatform.Data.Mapping.Builders.Identity
{
    public partial class AppUserLoginBuilder : DevPlatformEntityBuilder<AppUserLogin>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
              .WithColumn(nameof(AppUserLogin.LoginProvider)).AsString(256).NotNullable().PrimaryKey()
              .WithColumn(nameof(AppUserLogin.ProviderKey)).AsString(256).NotNullable().PrimaryKey()
              .WithColumn(nameof(AppUserLogin.ProviderDisplayName)).AsString(256).NotNullable()
              .WithColumn(nameof(AppUserLogin.UserId)).AsInt32().NotNullable().ForeignKey<AppUser>(onDelete: Rule.Cascade);
            #endregion
        }
    }
}
