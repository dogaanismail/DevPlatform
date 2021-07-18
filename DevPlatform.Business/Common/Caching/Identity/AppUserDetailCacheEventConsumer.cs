using DevPlatform.Core.Domain.Identity;
using System.Threading.Tasks;

namespace DevPlatform.Business.Common.Caching.Identity
{
    /// <summary>
    /// Represents an appUserDetail cache event consumer
    /// </summary>
    public partial class AppUserDetailCacheEventConsumer : CacheEventConsumer<AppUserDetail>
    {
        /// <summary>
        /// Clear cache data
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="entityEventType">Entity event type</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        protected override async Task ClearCacheAsync(AppUserDetail entity, EntityEventType entityEventType)
        {
            await base.ClearCacheAsync(entity, entityEventType);
        }
    }
}
