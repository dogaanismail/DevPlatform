using DevPlatform.Business.Common.CacheKeys.Identity;
using DevPlatform.Core.Domain.Identity;
using System.Threading.Tasks;

namespace DevPlatform.Business.Common.Caching.Identity
{
    /// <summary>
    /// Represents an appUser cache event consumer
    /// </summary>
    public partial class AppUserCacheEventConsumer : CacheEventConsumer<AppUser>
    {
        /// <summary>
        /// Clear cache data
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="entityEventType">Entity event type</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        protected override async Task ClearCacheAsync(AppUser entity, EntityEventType entityEventType)
        {
            if (entityEventType != EntityEventType.Insert)
            {
                await RemoveAsync(AppUserCacheKeys.UserDetailByUserNameCacheKey, entity.UserName);
                await RemoveAsync(AppUserCacheKeys.UserByUserNameCacheKey, entity.UserName);
                await RemoveAsync(AppUserCacheKeys.UserByEmailCacheKey, entity.Email);
            }

            await base.ClearCacheAsync(entity, entityEventType);
        }
    }
}
