﻿using DevPlatform.Core.Domain.Identity;
using FluentMigrator.Builders.Create.Table;
using DevPlatform.Data.Extensions;
using System.Data;

namespace DevPlatform.Data.Mapping.Builders.Identity
{
    public partial class AppUserRoleBuilder : DevPlatformEntityBuilder<AppUserRole>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
            .WithColumn(nameof(AppUserRole.UserId)).AsInt32().NotNullable().PrimaryKey().ForeignKey<AppUser>(onDelete: Rule.Cascade)
            .WithColumn(nameof(AppUserRole.RoleId)).AsInt32().NotNullable().PrimaryKey().ForeignKey<AppRole>(onDelete: Rule.Cascade);
            #endregion
        }
    }
}
