using System.Threading.Tasks;
using QuestionEntity = DevPlatform.Core.Domain.Question.Question;

namespace DevPlatform.Business.Common.Caching.Question
{
    /// <summary>
    /// Represents a question cache event consumer
    /// </summary>
    public partial class QuestionCacheEventConsumer : CacheEventConsumer<QuestionEntity>
    {
        /// <summary>
        /// Clear cache data
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="entityEventType">Entity event type</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        protected override async Task ClearCacheAsync(QuestionEntity entity, EntityEventType entityEventType)
        {
            await base.ClearCacheAsync(entity, entityEventType);
        }
    }
}
