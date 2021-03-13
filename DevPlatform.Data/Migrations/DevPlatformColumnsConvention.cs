﻿using FluentMigrator.Expressions;
using FluentMigrator.Runner.Conventions;

namespace DevPlatform.Data.Migrations
{
    /// <summary>
    /// Column type convention
    /// </summary>
    public class DevPlatformColumnsConvention : IColumnsConvention
    {
        #region Methods

        /// <summary>
        /// Applies a convention to a FluentMigrator.Expressions.IColumnsExpression
        /// </summary>
        /// <param name="expression">The expression this convention should be applied to</param>
        /// <returns>The same or a new expression. The underlying type must stay the same</returns>
        public IColumnsExpression Apply(IColumnsExpression expression)
        {
            var dataSettings = DataSettingsManager.LoadSettings();

            if (dataSettings.DataProvider == DataProviderType.PostgreSQL)
            {
                foreach (var columnDefinition in expression.Columns)
                {
                    if (columnDefinition.Type == System.Data.DbType.String)
                    {
                        columnDefinition.Type = null;
                        columnDefinition.CustomType = "citext";
                    }
                }
            }

            return expression;
        }

        #endregion
    }
}
