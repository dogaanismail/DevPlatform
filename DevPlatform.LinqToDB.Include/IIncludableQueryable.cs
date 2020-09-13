using LinqToDB.Linq;
using System;
using System.Linq.Expressions;

namespace DevPlatform.LinqToDB.Include
{
    public interface IIncludableQueryable<TClass> : IExpressionQuery<TClass> where TClass : class
    {
        IIncludableQueryable<TClass> AddExpression<TProperty>(Expression<Func<TClass, TProperty>> expr, Expression<Func<TProperty, bool>> propertyFilter = null)
            where TProperty : class;
    }
}
