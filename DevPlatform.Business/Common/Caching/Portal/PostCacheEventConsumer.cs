using DevPlatform.Core.Domain.Portal;
using System.Threading.Tasks;

namespace DevPlatform.Business.Common.Caching.Portal
{
    /// <summary>
    /// Represents a post cache event consumer
    /// </summary>
    public partial class PostCacheEventConsumer : CacheEventConsumer<Post>
    {
        /// <summary>
        /// Clear cache data
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="entityEventType">Entity event type</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        protected override async Task ClearCacheAsync(Post entity, EntityEventType entityEventType)
        {
            await base.ClearCacheAsync(entity, entityEventType);
        }
    }
}
