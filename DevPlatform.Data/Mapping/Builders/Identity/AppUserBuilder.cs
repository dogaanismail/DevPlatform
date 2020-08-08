using DevPlatform.Core.Domain.Identity;
using DevPlatform.Data.Extensions;
using FluentMigrator.Builders.Create.Table;
using System.Data;

namespace DevPlatform.Data.Mapping.Builders.Identity
{
    public partial class AppUserBuilder : DevPlatformEntityBuilder<AppUser>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(AppUser.UserName)).AsString(100).NotNullable()
                .WithColumn(nameof(AppUser.DetailId)).AsInt32().NotNullable().ForeignKey<AppUserDetail>(onDelete: Rule.Cascade);
        }
    }
}
