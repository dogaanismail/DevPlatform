using DevPlatform.Core.Caching;
using DevPlatform.Core.Domain.Question;
using System.Threading.Tasks;
using QuestionEntity = DevPlatform.Core.Domain.Question.Question;

namespace DevPlatform.Business.Common.Caching.Question
{
    /// <summary>
    /// Represents a questionComment cache event consumer
    /// </summary>
    public partial class QuestionCommentCacheEventConsumer : CacheEventConsumer<QuestionComment>
    {
        /// <summary>
        /// Clear cache data
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="entityEventType">Entity event type</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        protected override async Task ClearCacheAsync(QuestionComment entity, EntityEventType entityEventType)
        {
            await RemoveByPrefixAsync(DevPlatformEntityCacheDefaults<QuestionEntity>.AllPrefix);
            await RemoveAsync(DevPlatformEntityCacheDefaults<QuestionEntity>.ByIdCacheKey, entity.QuestionId);

            await base.ClearCacheAsync(entity, entityEventType);
        }
    }
}
