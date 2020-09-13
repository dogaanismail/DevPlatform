using System;
using System.Linq;
using System.Linq.Expressions;

namespace DevPlatform.LinqToDB.Include
{
    public static class IncludeExtensions
    {
        public static IIncludableQueryable<TClass> Include<TClass, TProperty>(
                this IQueryable<TClass> query,
                Expression<Func<TClass, TProperty>> expr,
                Expression<Func<TProperty, bool>> propertyFilter = null)
            where TClass : class
            where TProperty : class
                => new IncludableQueryable<TClass>(query).Include(expr, propertyFilter);

        public static IIncludableQueryable<TClass> Include<TClass, TProperty>(
                this IIncludableQueryable<TClass> includable,
                Expression<Func<TClass, TProperty>> expr,
                Expression<Func<TProperty, bool>> propertyFilter = null)
            where TClass : class
            where TProperty : class
                => includable.AddExpression(expr, propertyFilter);


        public static IIncludableQueryable<TClass> ToIncludableQueryable<TClass>(this IQueryable<TClass> query)
            where TClass : class
            => new IncludableQueryable<TClass>(query);
    }
}
