using DevPlatform.Core.Domain.Identity;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;
using FluentMigrator.SqlServer;

namespace DevPlatform.Data.Mapping.Builders.Identity
{
    public partial class AppUserDetailBuilder : DevPlatformEntityBuilder<AppUserDetail>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
                .WithColumn(nameof(AppUserDetail.FirstName)).AsString(128).NotNullable()
                .WithColumn(nameof(AppUserDetail.LastName)).AsString(128).NotNullable()
                .WithColumn(nameof(AppUserDetail.BirthDate)).AsDateTime().Nullable()
                .WithColumn(nameof(AppUserDetail.ProfilePhotoPath)).AsString(256).NotNullable()
                .WithColumn(nameof(AppUserDetail.CoverPhotoPath)).AsString(256).NotNullable()
                .WithColumn(nameof(AppUserDetail.City)).AsString(128).Nullable()
                .WithColumn(nameof(AppUserDetail.Country)).AsString(128).Nullable()
                .WithColumn(nameof(AppUserDetail.AboutMe)).AsString(512).Nullable()
                .WithColumn(nameof(AppUserDetail.Sex)).AsString(16).Nullable()
                .WithColumn(nameof(AppUserDetail.UniversityName)).AsString(128).Nullable()
                .WithColumn(nameof(AppUserDetail.UniStartDate)).AsDateTime().Nullable()
                .WithColumn(nameof(AppUserDetail.UniFinishUpDate)).AsDateTime().Nullable()
                .WithColumn(nameof(AppUserDetail.HasGraduated)).AsBoolean().Nullable()
                .WithColumn(nameof(AppUserDetail.UniversityDesc)).AsString(256).Nullable()
                .WithColumn(nameof(AppUserDetail.CompanyName)).AsString(128).Nullable()
                .WithColumn(nameof(AppUserDetail.Designation)).AsString(256).Nullable()
                .WithColumn(nameof(AppUserDetail.CreatedDate)).AsDateTime().NotNullable()
                .WithColumn(nameof(AppUserDetail.ModifiedDate)).AsDateTime().Nullable()
                .WithColumn(nameof(AppUserDetail.StatusId)).AsInt32().Nullable();
            #endregion
        }
    }
}
