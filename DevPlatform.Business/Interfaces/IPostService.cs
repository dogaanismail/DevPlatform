using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Api;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using DevPlatform.Domain.ServiceResponseModels.PostService;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task<ResultModel> CreateAsync(Post post);

        /// <summary>
        /// Inserts posts by using bulk
        /// </summary>
        /// <param name="post"></param>
        Task<ResultModel> CreateAsync(List<Post> posts);

        /// <summary>
        /// Inserts posts and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ServiceResponse<CreateResponse>> CreateAsync(PostCreateApi model);

        /// <summary>
        /// Deletes a post
        /// </summary>
        /// <param name="post"></param>
        Task DeleteAsync(Post post);

        /// <summary>
        /// Updates a post
        /// </summary>
        /// <param name="post"></param>
        Task UpdateAsync(Post post);

        /// <summary>
        /// Gets a post by id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        Task<Post> GetByIdAsync(int postId);

        /// <summary>
        /// Returns a post as Dto by PostId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PostListDto> GetByIdAsDtoAsync(int id);

        /// <summary>
        /// Returns the post lists
        /// </summary>
        /// <returns></returns>
        Task<List<PostListDto>> GetPostListAsync();

        /// <summary>
        /// Returns posts of user by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<Post>> GetUserPostsByUserIdAsync(int userId);

        /// <summary>
        /// Returns posts of user by userId with dto
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<PostListDto>> GetUserPostsWithDtoAsync(int userId);
    }
}
