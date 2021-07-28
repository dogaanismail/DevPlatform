using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevPlatform.Business.Interfaces
{
    /// <summary>
    /// Post image service interface
    /// </summary>
    public partial interface IPostImageService
    {
        /// <summary>
        /// Creates a post image for a post
        /// </summary>
        /// <param name="createImageForPost"></param>
        /// <returns></returns>
        Task<ResultModel> CreateAsync(PostImage createImageForPost);

        /// <summary>
        /// Creates post images by using bulk entities
        /// </summary>
        /// <param name="createImageForPost"></param>
        /// <returns></returns>
        Task<ResultModel> CreateAsync(List<PostImage> createImageForPost);
    }
}
