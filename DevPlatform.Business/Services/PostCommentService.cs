using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Api;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.ServiceResponseModels.PostCommentService;
using DevPlatform.Repository.Extensions;
using DevPlatform.Repository.Generic;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// Post comment service
    /// </summary>
    public partial class PostCommentService : ServiceExecute, IPostCommentService
    {
        #region Fields
        private readonly IRepository<PostComment> _postCommentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        #endregion

        #region Ctor
        public PostCommentService(IRepository<PostComment> postCommentRepository,
            IHttpContextAccessor httpContextAccessor,
            IUserService userService)
        {
            _postCommentRepository = postCommentRepository;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }
        #endregion

        /// <summary>
        /// Creates post comment
        /// </summary>
        /// <param name="createComment"></param>
        /// <returns></returns>
        public ResultModel Create(PostComment createComment)
        {
            if (createComment == null)
                throw new ArgumentNullException(nameof(createComment));

            _postCommentRepository.Insert(createComment);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        /// <summary>
        /// Deletes a post comment
        /// </summary>
        /// <param name="deleteComment"></param>
        /// <returns></returns>
        public ResultModel Delete(PostComment deleteComment)
        {
            if (deleteComment == null)
                throw new ArgumentNullException(nameof(deleteComment));

            _postCommentRepository.Delete(deleteComment);
            return new ResultModel { Status = true, Message = "Delete Process Success ! " };
        }

        /// <summary>
        /// Updates a post comment
        /// </summary>
        /// <param name="updateComment"></param>
        /// <returns></returns>
        public ResultModel Update(PostComment updateComment)
        {
            if (updateComment == null)
                throw new ArgumentNullException(nameof(updateComment));

            _postCommentRepository.Update(updateComment);
            return new ResultModel { Status = true, Message = "Update Process Success ! " };
        }

        /// <summary>
        /// Returnsa post comment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PostComment GetById(int id)
        {
            return _postCommentRepository.GetById(id);
        }

        /// <summary>
        /// Returns a list of post comment by post id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public List<PostComment> GetPostCommentsByPostId(int postId)
        {
            return _postCommentRepository.GetList(x => x.PostId == postId, x => x.Include(t => t.CreatedUser).ThenInclude(e => e.UserDetail));
        }

        /// <summary>
        /// Creates a post comment and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ServiceResponse<CreateResponse> Create(PostCommentCreateApi model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var serviceResponse = new ServiceResponse<CreateResponse>
            {
                Success = false
            };

            try
            {
                if (model.PostId == 0)
                    return ServiceResponse((CreateResponse)null, new List<string> { "Text can not be null !" });

                if (string.IsNullOrEmpty(model.Text))
                    return ServiceResponse((CreateResponse)null, new List<string> { "Text can not be null !" });

                var commentPost = _postService.GetById(model.PostId);

                if (commentPost == null)
                    return ServiceResponse((CreateResponse)null, new List<string> { "Post not found!" });

                var appUser = _userService.FindByUserName(_httpContextAccessor.HttpContext.User.Identity.Name);

                if (appUser == null)
                    return ServiceResponse((CreateResponse)null, new List<string> { "User not found!" });

                PostComment newComment = new()
                {
                    PostId = model.PostId,
                    Text = model.Text,
                    CreatedBy = appUser.Id
                };

                ResultModel result = Create(newComment);

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
                    CreatedByUserPhoto = appUser.UserDetail?.ProfilePhotoPath
                };

                return serviceResponse;

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Warnings.Add(ex.Message);
                return serviceResponse;
            }
        }
    }
}
