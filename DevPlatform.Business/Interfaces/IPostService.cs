using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using System.Collections.Generic;

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
        ResultModel Create(Post post);

        /// <summary>
        /// Inserts posts by using bulk
        /// </summary>
        /// <param name="post"></param>
        ResultModel Create(List<Post> posts);

        /// <summary>
        /// Deletes a post
        /// </summary>
        /// <param name="post"></param>
        void Delete(Post post);

        /// <summary>
        /// Updates a post
        /// </summary>
        /// <param name="post"></param>
        void Update(Post post);

        /// <summary>
        /// Gets a post by id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        Post GetById(int postId);

        /// <summary>
        /// Returns a post as Dto by PostId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PostListDto GetByIdAsDto(int id);

        /// <summary>
        /// Returns the post lists
        /// </summary>
        /// <returns></returns>
        IEnumerable<PostListDto> GetPostList();

        /// <summary>
        /// Returns posts of user by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<Post> GetUserPostsByUserId(int userId);

        /// <summary>
        /// Returns posts of user by userId with dto
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<PostListDto> GetUserPostsWithDto(int userId);
    }
}
