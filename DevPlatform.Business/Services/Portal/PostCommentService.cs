using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Api;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Enumerations;
using DevPlatform.Domain.ServiceResponseModels.PostCommentService;
using DevPlatform.Repository.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinqToDB;
using System.Linq;
using DevPlatform.Business.Interfaces.Portal;
using DevPlatform.Business.Interfaces.Logging;
using DevPlatform.Business.Interfaces.Identity;

namespace DevPlatform.Business.Services.Portal
{
    /// <summary>
    /// Post comment service
    /// </summary>
    public partial class PostCommentService : ServiceExecute, IPostCommentService
    {
        #region Fields
        private readonly IRepository<PostComment> _postCommentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<AppUser> _userRepository;
        private readonly ILogService _logService;
        private readonly IUserDetailService _userDetailService;
        #endregion

        #region Ctor
        public PostCommentService(IRepository<PostComment> postCommentRepository,
            IHttpContextAccessor httpContextAccessor,
            IRepository<AppUser> userRepository,
            IRepository<Post> postRepository,
            ILogService logService,
            IUserDetailService userDetailService)
        {
            _postCommentRepository = postCommentRepository;
            _httpContextAccessor = httpContextAccessor;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _logService = logService;
            _userDetailService = userDetailService;
        }
        #endregion

        /// <summary>
        /// Creates post comment
        /// </summary>
        /// <param name="createComment"></param>
        /// <returns></returns>
        public virtual async Task<ResultModel> CreateAsync(PostComment createComment)
        {
            if (createComment == null)
                throw new ArgumentNullException(nameof(createComment));

            await _postCommentRepository.InsertAsync(createComment);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        /// <summary>
        /// Creates a post comment and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task<ServiceResponse<CreateResponse>> CreateAsync(PostCommentCreateApi model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var serviceResponse = new ServiceResponse<CreateResponse>
            {
                Success = false
            };

            try
            {
                if (model.PostId < 1)
                    return ServiceResponse((CreateResponse)null, new List<string> { "PostId can not be null !" });

                if (string.IsNullOrEmpty(model.Text))
                    return ServiceResponse((CreateResponse)null, new List<string> { "Text can not be null !" });

                var commentPost = await _postRepository.Table.FirstOrDefaultAsync(p => p.Id == model.PostId);

                if (commentPost == null)
                    return ServiceResponse((CreateResponse)null, new List<string> { "Post not found!" });

                var appUser = await _userDetailService.GetUserDetailByUserNameAsync(_httpContextAccessor.HttpContext?.User?.Identity?.Name);

                if (appUser == null)
                    return ServiceResponse((CreateResponse)null, new List<string> { "User not found!" });

                PostComment newComment = new()
                {
                    PostId = commentPost.Id,
                    Text = model.Text,
                    CreatedBy = appUser.Id
                };

                ResultModel result = await CreateAsync(newComment);

                if (!result.Status)
                    return ServiceResponse((CreateResponse)null, new List<string> { result.Message });

                serviceResponse.Success = true;
                serviceResponse.Data = new CreateResponse
                {
                    Text = newComment.Text,
                    Id = newComment.Id,
                    PostId = newComment.PostId,
                    CreatedDate = newComment.CreatedDate,
                    CreatedByUserName = appUser.UserName,
                    CreatedByUserPhoto = appUser.ProfilePhotoUrl
                };

                return serviceResponse;

            }
            catch (Exception ex)
            {
                await _logService.InsertLogAsync(LogLevel.Error, $"PostCommentService- Create Error: model {JsonConvert.SerializeObject(model)}", ex.Message.ToString());
                serviceResponse.Success = false;
                serviceResponse.Warnings.Add(ex.Message);
                return serviceResponse;
            }
        }

        /// <summary>
        /// Deletes a post comment
        /// </summary>
        /// <param name="deleteComment"></param>
        /// <returns></returns>
        public virtual async Task<ResultModel> DeleteAsync(PostComment deleteComment)
        {
            if (deleteComment == null)
                throw new ArgumentNullException(nameof(deleteComment));

            await _postCommentRepository.DeleteAsync(deleteComment);
            return new ResultModel { Status = true, Message = "Delete Process Success ! " };
        }

        /// <summary>
        /// Updates a post comment
        /// </summary>
        /// <param name="updateComment"></param>
        /// <returns></returns>
        public virtual async Task<ResultModel> UpdateAsync(PostComment updateComment)
        {
            if (updateComment == null)
                throw new ArgumentNullException(nameof(updateComment));

            await _postCommentRepository.UpdateAsync(updateComment);
            return new ResultModel { Status = true, Message = "Update Process Success ! " };
        }

        /// <summary>
        /// Returns a post comment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<PostComment> GetByIdAsync(int id)
        {
            return await _postCommentRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Returns a list of post comment by post id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public virtual async Task<List<PostComment>> GetPostCommentsByPostIdAsync(int postId)
        {
            return await _postCommentRepository.Table.Where(comment => comment.PostId == postId)
                .LoadWith(x => x.CreatedUser)
                .ThenLoad(x => x.UserDetail)
                .ToListAsyncMethod();
        }
    }
}
