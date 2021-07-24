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
        Task<ResultModel> Create(Post post);

        /// <summary>
        /// Inserts posts by using bulk
        /// </summary>
        /// <param name="post"></param>
        Task<ResultModel> Create(List<Post> posts);

        /// <summary>
        /// Deletes a post
        /// </summary>
        /// <param name="post"></param>
        Task Delete(Post post);

        /// <summary>
        /// Updates a post
        /// </summary>
        /// <param name="post"></param>
        Task Update(Post post);

        /// <summary>
        /// Gets a post by id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        Task<Post> GetById(int postId);

        /// <summary>
        /// Returns a post as Dto by PostId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PostListDto> GetByIdAsDto(int id);

        /// <summary>
        /// Returns the post lists
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<PostListDto>> GetPostList();

        /// <summary>
        /// Returns posts of user by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<Post>> GetUserPostsByUserId(int userId);

        /// <summary>
        /// Returns posts of user by userId with dto
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<PostListDto>> GetUserPostsWithDto(int userId);

        /// <summary>
        /// Inserts posts and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ServiceResponse<CreateResponse>> Create(PostCreateApi model);
    }
}
