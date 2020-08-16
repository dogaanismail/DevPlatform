using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Common;

namespace DevPlatform.Business.Interfaces
{
    /// <summary>
    /// Post service interface
    /// </summary>
    public partial interface IPostService
    {
        /// <summary>
        /// Inserts a post
        /// </summary>
        /// <param name="post"></param>
        ResultModel InsertPost(Post post);

        /// <summary>
        /// Deletes a post
        /// </summary>
        /// <param name="post"></param>
        void DeletePost(Post post);

        /// <summary>
        /// Updates a post
        /// </summary>
        /// <param name="post"></param>
        void UpdatePost(Post post);

        /// <summary>
        /// Gets a post by id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        Post GetById(int postId);
    }
}
