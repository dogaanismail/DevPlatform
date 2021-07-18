using DevPlatform.Core.Caching;
using DevPlatform.Core.Domain.Story;
using System.Threading.Tasks;
using StoryEntity = DevPlatform.Core.Domain.Story.Story;

namespace DevPlatform.Business.Common.Caching.Story
{
    /// <summary>
    /// Represents a storyComment cache event consumer
    /// </summary>
    public partial class QuestionCommentCacheEventConsumer : CacheEventConsumer<StoryComment>
    {
        /// <summary>
        /// Clear cache data
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="entityEventType">Entity event type</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        protected override async Task ClearCacheAsync(StoryComment entity, EntityEventType entityEventType)
        {
            await RemoveByPrefixAsync(DevPlatformEntityCacheDefaults<StoryEntity>.AllPrefix);
            await RemoveAsync(DevPlatformEntityCacheDefaults<StoryEntity>.ByIdCacheKey, entity.StoryId);

            await base.ClearCacheAsync(entity, entityEventType);
        }
    }
}
