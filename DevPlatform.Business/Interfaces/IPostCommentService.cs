using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Api;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.ServiceResponseModels.PostCommentService;
using System.Collections.Generic;

namespace DevPlatform.Business.Interfaces
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
        ResultModel Create(PostComment createComment);

        /// <summary>
        /// Deletes a post comment
        /// </summary>
        /// <param name="deleteComment"></param>
        /// <returns></returns>
        ResultModel Delete(PostComment deleteComment);

        /// <summary>
        /// Updates a post comment
        /// </summary>
        /// <param name="updateComment"></param>
        /// <returns></returns>
        ResultModel Update(PostComment updateComment);

        /// <summary>
        /// Return post comment by comment id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PostComment GetById(int id);

        /// <summary>
        /// Returns post comment list by post Id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        List<PostComment> GetPostCommentsByPostId(int postId);

        /// <summary>
        /// Creates a post comment and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ServiceResponse<CreateResponse> Create(PostCommentCreateApi model);
    }
}
