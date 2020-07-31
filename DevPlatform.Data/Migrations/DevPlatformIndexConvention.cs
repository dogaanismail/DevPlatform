using FluentMigrator.Expressions;
using FluentMigrator.Model;
using FluentMigrator.Runner.Conventions;
using System.Linq;

namespace DevPlatform.Data.Migrations
{
    /// <summary>
    /// Convention for the default naming of an index
    /// </summary>
    public class DevPlatformIndexConvention : IIndexConvention
    {
        #region Fields

        private readonly IDevPlatformDataProvider _dataProvider;

        #endregion

        #region Ctor

        public DevPlatformIndexConvention(IDevPlatformDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        #endregion

        #region Utils

        /// <summary>
        /// Gets the default name of an index
        /// </summary>
        /// <param name="index">The index definition</param>
        /// <returns>Name of an index</returns>
        private string GetIndexName(IndexDefinition index)
        {
            return _dataProvider.GetIndexName(index.TableName, string.Join('_', index.Columns.Select(c => c.Name)));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Applies a convention to a FluentMigrator.Expressions.IIndexExpression
        /// </summary>
        /// <param name="expression">The expression this convention should be applied to</param>
        /// <returns>The same or a new expression. The underlying type must stay the same.</returns>
        public IIndexExpression Apply(IIndexExpression expression)
        {
            if (string.IsNullOrEmpty(expression.Index.Name))
                expression.Index.Name = GetIndexName(expression.Index);

            return expression;
        }

        #endregion
    }
}
