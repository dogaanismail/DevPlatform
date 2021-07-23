using DevPlatform.Core.Caching;

namespace DevPlatform.Business.Common.CacheKeys.Identity
{
    public static partial class AppUserCacheKeys
    {
        #region UserDetail

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : userName
        /// </remarks>
        public static CacheKey UserDetailByUserNameCacheKey => new("DevPlatform.userdetail.byusername.{0}");


        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : userName
        /// </remarks>
        public static CacheKey UserDetailForProfileByUserNameCacheKey => new("DevPlatform.profile.byusername.{0}");

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : userName
        /// </remarks>
        public static CacheKey UserByUserNameCacheKey => new("DevPlatform.user.byusername.{0}");

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : email
        /// </remarks>
        public static CacheKey UserByEmailCacheKey => new("DevPlatform.user.email.{0}");

        #endregion
    }
}
