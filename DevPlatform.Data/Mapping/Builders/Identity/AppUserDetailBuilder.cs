using DevPlatform.Core.Domain.Identity;
using FluentMigrator.Builders.Create.Table;

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
                .WithColumn(nameof(AppUserDetail.City)).AsString(128).NotNullable()
                .WithColumn(nameof(AppUserDetail.Country)).AsString(128).NotNullable()
                .WithColumn(nameof(AppUserDetail.AboutMe)).AsString(512).NotNullable()
                .WithColumn(nameof(AppUserDetail.Sex)).AsString(16).NotNullable()
                .WithColumn(nameof(AppUserDetail.UniversityName)).AsString(128).NotNullable()
                .WithColumn(nameof(AppUserDetail.UniStartDate)).AsDateTime().Nullable()
                .WithColumn(nameof(AppUserDetail.UniFinishUpDate)).AsDateTime().Nullable()
                .WithColumn(nameof(AppUserDetail.HasGraduated)).AsBoolean().Nullable()
                .WithColumn(nameof(AppUserDetail.UniversityDesc)).AsString(256).NotNullable()
                .WithColumn(nameof(AppUserDetail.CompanyName)).AsString(128).NotNullable()
                .WithColumn(nameof(AppUserDetail.Designation)).AsString(256).NotNullable();
            #endregion
        }
    }
}
