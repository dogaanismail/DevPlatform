﻿using DevPlatform.Core.Domain.Identity;
using FluentMigrator.Builders.Create.Table;

namespace DevPlatform.Data.Mapping.Builders.Identity
{
    public partial class AppRoleBuilder : DevPlatformEntityBuilder<AppRole>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
                .WithColumn(nameof(AppRole.Name)).AsString(128).NotNullable()
                .WithColumn(nameof(AppRole.NormalizedName)).AsString(128).NotNullable()
                .WithColumn(nameof(AppRole.ConcurrencyStamp)).AsString(256).NotNullable();
            #endregion
        }
    }
}
