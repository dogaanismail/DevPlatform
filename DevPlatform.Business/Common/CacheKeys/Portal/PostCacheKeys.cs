using DevPlatform.Core.Caching;
using DevPlatform.Core.Domain.Portal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevPlatform.Business.Common.CacheKeys.Portal
{
    public static partial class PostCacheKeys
    {
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : current store ID
        /// {1} : roles of the current user
        /// {2} : show hidden records?
        /// </remarks>
        public static CacheKey PostsAllCacheKey => new CacheKey("DevPlatform.post.all.", DevPlatformEntityCacheDefaults<Post>.AllPrefix);
    }
}
