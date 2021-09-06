using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Api;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.ServiceResponseModels.PostCommentService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevPlatform.Business.Interfaces.Portal
{
    /// <summary>
    /// Post comment service interface
    /// </summary>
    public partial interface IPostCommentService
    {
        /// <summary>
        /// Creates post comments for a post.
        /// </summary>
        /// <param name="createComment"></param>
        /// <returns></returns>
        Task<ResultModel> CreateAsync(PostComment createComment);

        /// <summary>
        /// Creates a post comment and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ServiceResponse<CreateResponse>> CreateAsync(PostCommentCreateApi model);

        /// <summary>
        /// Deletes a post comment
        /// </summary>
        /// <param name="deleteComment"></param>
        /// <returns></returns>
        Task<ResultModel> DeleteAsync(PostComment deleteComment);

        /// <summary>
        /// Updates a post comment
        /// </summary>
        /// <param name="updateComment"></param>
        /// <returns></returns>
        Task<ResultModel> UpdateAsync(PostComment updateComment);

        /// <summary>
        /// Return post comment by comment id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PostComment> GetByIdAsync(int id);

        /// <summary>
        /// Returns post comment list by post Id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        Task<List<PostComment>> GetPostCommentsByPostIdAsync(int postId);
    }
}
