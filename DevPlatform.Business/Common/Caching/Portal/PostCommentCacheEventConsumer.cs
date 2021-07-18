using DevPlatform.Core.Caching;
using DevPlatform.Core.Domain.Portal;
using System.Threading.Tasks;

namespace DevPlatform.Business.Common.Caching.Portal
{
    /// <summary>
    /// Represents a postComment cache event consumer
    /// </summary>
    public partial class PostCommentCacheEventConsumer : CacheEventConsumer<PostComment>
    {
        /// <summary>
        /// Clear cache data
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="entityEventType">Entity event type</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        protected override async Task ClearCacheAsync(PostComment entity, EntityEventType entityEventType)
        {
            await RemoveByPrefixAsync(DevPlatformEntityCacheDefaults<Post>.AllPrefix);
            await RemoveAsync(DevPlatformEntityCacheDefaults<Post>.ByIdCacheKey, entity.PostId);

            await base.ClearCacheAsync(entity, entityEventType);
        }
    }
}
