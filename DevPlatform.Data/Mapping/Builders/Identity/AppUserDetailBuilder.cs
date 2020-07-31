using DevPlatform.Core.Domain.Identity;
using FluentMigrator.Builders.Create.Table;

namespace DevPlatform.Data.Mapping.Builders.Identity
{
    public partial class AppUserDetailBuilder : DevPlatformEntityBuilder<AppUserDetail>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(AppUserDetail.CoverPhotoPath)).AsString(150).NotNullable();
        }
    }
}
