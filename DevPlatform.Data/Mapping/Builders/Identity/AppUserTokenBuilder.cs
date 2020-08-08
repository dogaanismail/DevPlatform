using DevPlatform.Core.Domain.Identity;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;

namespace DevPlatform.Data.Mapping.Builders.Identity
{
    public partial class AppUserTokenBuilder : DevPlatformEntityBuilder<AppUserToken>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
            .WithColumn(nameof(AppUserToken.UserId)).AsInt32().NotNullable().PrimaryKey().ForeignKey<AppUser>(onDelete: Rule.Cascade)
            .WithColumn(nameof(AppUserToken.LoginProvider)).AsString(256).NotNullable().PrimaryKey()
            .WithColumn(nameof(AppUserToken.Name)).AsString(256).NotNullable().PrimaryKey()
            .WithColumn(nameof(AppUserToken.Value)).AsString(256).NotNullable();
            #endregion
        }
    }
}
