using LinqToDB;
using System;
using System.Linq;
using LinqToDB.Linq;

namespace DevPlatform.LinqToDB.Include
{
    public static class QueryableExtensions
    {
        public static T GetDataContext<T>(this IQueryable query) where T : IDataContext
        {
            var expressionQuery = query as IExpressionQuery;
            if (!(expressionQuery?.DataContext is T))
            {
                throw new InvalidCastException($"DataContext '{typeof(T).Name}' not found");
            }

            return (T)expressionQuery.DataContext;
        }
    }
}
