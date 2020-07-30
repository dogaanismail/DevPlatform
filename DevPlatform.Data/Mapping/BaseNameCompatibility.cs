using System;
using System.Collections.Generic;
using System.Text;

namespace DevPlatform.Data.Mapping
{
    /// <summary>
    /// Base instance of backward compatibility of table naming
    /// </summary>
    public partial class BaseNameCompatibility : INameCompatibility
    {
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>
        {
            //{ typeof(ForumPost), "Forums_Post" },
        };

        public Dictionary<(Type, string), string> ColumnName => new Dictionary<(Type, string), string>
        {
            //{ (typeof(Post), "CreatedBy"), "CreatedBy_Id" },
        };
    }
}
