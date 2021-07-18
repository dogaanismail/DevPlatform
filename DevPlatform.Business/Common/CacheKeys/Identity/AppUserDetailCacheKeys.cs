using DevPlatform.Core.Caching;

namespace DevPlatform.Business.Common.CacheKeys.Identity
{
    public static partial class AppUserDetailCacheKeys
    {
        #region UserDetail
        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : userName
        /// </remarks>
        public static CacheKey UserDetailByUserNameCacheKey => new("DevPlatform.userdetail.byusername.{0}");

        #endregion
    }
}
