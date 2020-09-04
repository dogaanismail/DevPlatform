using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Common;

namespace DevPlatform.Business.Interfaces
{
    /// <summary>
    /// Post image service interface
    /// </summary>
    public interface IPostImageService
    {
        /// <summary>
        /// Creates post images for a post
        /// </summary>
        /// <param name="createImageForPost"></param>
        /// <returns></returns>
        ResultModel Create(PostImage createImageForPost);
    }
}
