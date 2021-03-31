using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Domain.Logging;
using DevPlatform.Data.Extensions;
using FluentMigrator.Builders.Create.Table;
using System.Data;

namespace DevPlatform.Data.Mapping.Builders.Logging
{
    public partial class LogBuilder : DevPlatformEntityBuilder<Log>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            #region Methods
            table
            .WithColumn(nameof(Log.LogLevelId)).AsInt16().NotNullable()
            .WithColumn(nameof(Log.ShortMessage)).AsString(int.MaxValue).NotNullable()
            .WithColumn(nameof(Log.FullMessage)).AsString(int.MaxValue).NotNullable()
            .WithColumn(nameof(Log.IpAddress)).AsString(128).Nullable()
            .WithColumn(nameof(Log.CustomerId)).AsInt32().Nullable()
            .WithColumn(nameof(Log.PageUrl)).AsString(256).Nullable()
            .WithColumn(nameof(Log.ReferrerUrl)).AsString(256).Nullable()
            .WithColumn(nameof(Log.CreatedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
            .WithColumn(nameof(Log.ModifiedBy)).AsInt32().Nullable().ForeignKey<AppUser>(onDelete: Rule.None)
            .WithColumn(nameof(Log.CreatedDate)).AsDateTime().NotNullable()
            .WithColumn(nameof(Log.ModifiedDate)).AsDateTime().Nullable()
            .WithColumn(nameof(Log.StatusId)).AsInt32().Nullable();
            #endregion
        }
    }
}
