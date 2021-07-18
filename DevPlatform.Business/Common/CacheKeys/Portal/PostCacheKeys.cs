using DevPlatform.Core.Caching;
using DevPlatform.Core.Domain.Portal;

namespace DevPlatform.Business.Common.CacheKeys.Portal
{
    public static partial class PostCacheKeys
    {
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// </remarks>
        public static CacheKey PostsAllCacheKey => new CacheKey("DevPlatform.post.all.", DevPlatformEntityCacheDefaults<Post>.AllPrefix);
    }
}
