using LinqToDB.Mapping;
using System.Collections.Generic;

namespace DevPlatform.LinqToDB.Include.Accessors
{
    interface IRootAccessor
    {
        PropertyAccessor<TEntity, TProperty> GetByPath<TEntity, TProperty>(List<string> pathParts) where TEntity : class where TProperty : class;
        IPropertyAccessor GetByPath(List<string> pathParts);
        MappingSchema MappingSchema { get; }
    }
}
