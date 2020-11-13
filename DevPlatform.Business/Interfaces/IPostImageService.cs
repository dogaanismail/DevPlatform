using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Common;
using System.Collections.Generic;

namespace DevPlatform.Business.Interfaces
{
    /// <summary>
    /// Post image service interface
    /// </summary>
    public partial interface IPostImageService
    {
        /// <summary>
        /// Creates post images for a post
        /// </summary>
        /// <param name="createImageForPost"></param>
        /// <returns></returns>
        ResultModel Create(PostImage createImageForPost);

        /// <summary>
        /// Creates post images by using bulk entities
        /// </summary>
        /// <param name="createImageForPost"></param>
        /// <returns></returns>
        ResultModel Create(List<PostImage> createImageForPost);


    }
}
