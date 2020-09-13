using DevPlatform.LinqToDB.Include.Setters;
using System.Collections.Generic;
using System.Linq;

namespace DevPlatform.LinqToDB.Include.Accessors
{
    interface IRootAccessor<TClass> : IRootAccessor where TClass : class
    {
        HashSet<IPropertyAccessor<TClass>> Properties { get; }
        void LoadMap(List<TClass> entities, IQueryable<TClass> query, Builder builder);
    }
}
