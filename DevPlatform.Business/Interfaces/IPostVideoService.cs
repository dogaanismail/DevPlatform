using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Common;

namespace DevPlatform.Business.Interfaces
{
    /// <summary>
    /// Post video service interface
    /// </summary>
    public partial interface IPostVideoService
    {
        /// <summary>
        /// Creates post videos for a post
        /// </summary>
        /// <param name="createVideoForPost"></param>
        /// <returns></returns>
        ResultModel Create(PostVideo createVideoForPost);
    }
}
