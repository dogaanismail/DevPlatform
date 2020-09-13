using System;
using System.Collections.Generic;

namespace DevPlatform.LinqToDB.Include.Accessors
{
    interface IPropertyAccessor
    {
        Type DeclaringType { get; }
        Type MemberType { get; }
        Type MemberEntityType { get; }
        HashSet<IPropertyAccessor> Properties { get; }
        string PropertyName { get; }
        bool IsMemberTypeIEnumerable { get; }


        IPropertyAccessor FindAccessor(List<string> pathParts);
    }
}
