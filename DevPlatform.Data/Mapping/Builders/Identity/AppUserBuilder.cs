using DevPlatform.Core.Domain.Identity;
using FluentMigrator.Builders.Create.Table;

namespace DevPlatform.Data.Mapping.Builders.Identity
{
    public partial class AppUserBuilder : DevPlatformEntityBuilder<AppUser>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(AppUser.UserName)).AsString(100).NotNullable();
        }
    }
}
