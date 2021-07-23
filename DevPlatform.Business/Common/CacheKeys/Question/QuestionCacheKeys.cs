using DevPlatform.Core.Caching;
using QuestionEntity = DevPlatform.Core.Domain.Question.Question;

namespace DevPlatform.Business.Common.CacheKeys.Question
{
    public static partial class QuestionCacheKeys
    {
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// </remarks>
        public static CacheKey QuestionsAllCacheKey => new("DevPlatform.question.all.", DevPlatformEntityCacheDefaults<QuestionEntity>.AllPrefix);
    }
}
