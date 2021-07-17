using System.Threading.Tasks;
using StoryEntity = DevPlatform.Core.Domain.Story.Story;

namespace DevPlatform.Business.Common.Caching.Story
{
    /// <summary>
    /// Represents a category cache event consumer
    /// </summary>
    public partial class QuestionCacheEventConsumer : CacheEventConsumer<StoryEntity>
    {
        /// <summary>
        /// Clear cache data
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="entityEventType">Entity event type</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        protected override async Task ClearCacheAsync(StoryEntity entity, EntityEventType entityEventType)
        {
            await base.ClearCacheAsync(entity, entityEventType);
        }
    }
}
