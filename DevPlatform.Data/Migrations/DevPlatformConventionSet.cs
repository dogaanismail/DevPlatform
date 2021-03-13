using FluentMigrator.Runner;
using FluentMigrator.Runner.Conventions;
using System;
using System.Collections.Generic;

namespace DevPlatform.Data.Migrations
{
    /// <summary>
    /// A set conventions to be applied to expressions
    /// </summary>
    public class DevPlatformConventionSet : IConventionSet
    {
        #region Ctor

        public DevPlatformConventionSet(IDevPlatformDataProvider dataProvider)
        {
            if (dataProvider is null)
                throw new ArgumentNullException(nameof(dataProvider));

            var defaultConventionSet = new DefaultConventionSet();

            ForeignKeyConventions = new List<IForeignKeyConvention>()
            {
                new DevPlatformForeignKeyConvention(dataProvider),
                defaultConventionSet.SchemaConvention,
            };

            IndexConventions = new List<IIndexConvention>()
            {
                new DevPlatformIndexConvention(dataProvider),
                defaultConventionSet.SchemaConvention
            };

            ColumnsConventions = new List<IColumnsConvention>()
            {
                new DevPlatformColumnsConvention(),
                new DefaultPrimaryKeyNameConvention()
            };

            ConstraintConventions = defaultConventionSet.ConstraintConventions;

            SequenceConventions = defaultConventionSet.SequenceConventions;
            AutoNameConventions = defaultConventionSet.AutoNameConventions;
            SchemaConvention = defaultConventionSet.SchemaConvention;
            RootPathConvention = defaultConventionSet.RootPathConvention;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the root path convention to be applied to <see cref="T:FluentMigrator.Expressions.IFileSystemExpression" /> implementations
        /// </summary>
        public IRootPathConvention RootPathConvention { get; }

        /// <summary>
        /// Gets the default schema name convention to be applied to <see cref="T:FluentMigrator.Expressions.ISchemaExpression" /> implementations
        /// </summary>
        /// <remarks>
        /// This class cannot be overridden. The <see cref="T:FluentMigrator.Runner.Conventions.IDefaultSchemaNameConvention" />
        /// must be implemented/provided instead.
        /// </remarks>
        public DefaultSchemaConvention SchemaConvention { get; }

        /// <summary>
        /// Gets the conventions to be applied to <see cref="T:FluentMigrator.Expressions.IColumnsExpression" /> implementations
        /// </summary>
        public IList<IColumnsConvention> ColumnsConventions { get; }

        /// <summary>
        /// Gets the conventions to be applied to <see cref="T:FluentMigrator.Expressions.IConstraintExpression" /> implementations
        /// </summary>
        public IList<IConstraintConvention> ConstraintConventions { get; }

        /// <summary>
        /// Gets the conventions to be applied to <see cref="T:FluentMigrator.Expressions.IForeignKeyExpression" /> implementations
        /// </summary>
        public IList<IForeignKeyConvention> ForeignKeyConventions { get; }

        /// <summary>
        /// Gets the conventions to be applied to <see cref="T:FluentMigrator.Expressions.IIndexExpression" /> implementations
        /// </summary>
        public IList<IIndexConvention> IndexConventions { get; }

        /// <summary>
        /// Gets the conventions to be applied to <see cref="T:FluentMigrator.Expressions.ISequenceExpression" /> implementations
        /// </summary>
        public IList<ISequenceConvention> SequenceConventions { get; }

        /// <summary>
        /// Gets the conventions to be applied to <see cref="T:FluentMigrator.Expressions.IAutoNameExpression" /> implementations
        /// </summary>
        public IList<IAutoNameConvention> AutoNameConventions { get; }

        #endregion
    }
}
