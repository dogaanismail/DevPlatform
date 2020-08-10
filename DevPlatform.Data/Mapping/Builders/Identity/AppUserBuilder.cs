using DevPlatform.Core.Domain.Identity;
using DevPlatform.Data.Extensions;
using FluentMigrator.Builders.Create.Table;
using FluentMigrator.SqlServer;
using System.Data;

namespace DevPlatform.Data.Mapping.Builders.Identity
{
    public partial class AppUserBuilder : DevPlatformEntityBuilder<AppUser>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(AppUser.Id)).AsInt32().NotNullable().PrimaryKey().Identity(1,1)
                .WithColumn(nameof(AppUser.UserName)).AsString(128).NotNullable()
                .WithColumn(nameof(AppUser.NormalizedUserName)).AsString(128).NotNullable()
                .WithColumn(nameof(AppUser.Email)).AsString(128).NotNullable()
                .WithColumn(nameof(AppUser.NormalizedEmail)).AsString(128).NotNullable()
                .WithColumn(nameof(AppUser.EmailConfirmed)).AsBoolean().NotNullable()
                .WithColumn(nameof(AppUser.PasswordHash)).AsString(int.MaxValue).NotNullable()
                .WithColumn(nameof(AppUser.SecurityStamp)).AsString(int.MaxValue).NotNullable()
                .WithColumn(nameof(AppUser.ConcurrencyStamp)).AsString(int.MaxValue).NotNullable()
                .WithColumn(nameof(AppUser.PhoneNumber)).AsString(int.MaxValue).NotNullable()
                .WithColumn(nameof(AppUser.PhoneNumberConfirmed)).AsBoolean().Nullable()
                .WithColumn(nameof(AppUser.TwoFactorEnabled)).AsBoolean().NotNullable()
                .WithColumn(nameof(AppUser.LockoutEnd)).AsDateTimeOffset(7).Nullable()
                .WithColumn(nameof(AppUser.LockoutEnabled)).AsBoolean().NotNullable()
                .WithColumn(nameof(AppUser.AccessFailedCount)).AsInt32().NotNullable()
                .WithColumn(nameof(AppUser.DetailId)).AsInt32().Nullable().ForeignKey<AppUserDetail>(onDelete: Rule.Cascade);
        }
    }
}
