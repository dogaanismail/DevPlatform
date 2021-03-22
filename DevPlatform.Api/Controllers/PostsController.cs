using DevPlatform.Business.Interfaces;
using DevPlatform.Domain.Api;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DevPlatform.Api.Controllers
{
    [ApiController]
    public partial class PostsController : BaseApiController
    {
        #region Fields
        private readonly IPostService _postService;
        private readonly IPostCommentService _postCommentService;
        #endregion

        #region Ctor
        public PostsController(IPostService postService,
            IPostCommentService postCommentService)
        {
            _postService = postService;
            _postCommentService = postCommentService;
        }
        #endregion

        #region Methods

        [HttpPost("createpost")]
        [Authorize]
        public virtual JsonResult CreatePost([FromForm] PostCreateApi model)
        {
            var serviceResponse = _postService.Create(model);

            if (serviceResponse.Warnings.Count > 0 || serviceResponse.Warnings.Any())
                return BadResponse(new ResultModel
                {
                    Status = false,
                    Message = serviceResponse.Warnings.First()
                });

            return OkResponse(new PostListDto
            {
                Id = serviceResponse.Data.Id,
                Text = serviceResponse.Data?.Text,
                ImageUrl = serviceResponse.Data.ImageUrl?.ToString(),
                CreatedByUserName = serviceResponse.Data?.CreatedByUserName,
                CreatedByUserPhoto = serviceResponse.Data?.CreatedByUserPhoto,
                CreatedDate = serviceResponse.Data.CreatedDate,
                VideoUrl = serviceResponse.Data?.VideoUrl,
                PostType = serviceResponse.Data?.PostType,
                Comments = null
            });
        }

        [HttpGet("postlist")]
        [AllowAnonymous]
        public virtual JsonResult GetPostList()
        {
            var data = _postService.GetPostList();
            return OkResponse(data);
        }

        [HttpPost("createcomment")]
        [Authorize]
        public virtual JsonResult CreatePostComment([FromBody] PostCommentCreateApi model)
        {
            var serviceResponse = _postCommentService.Create(model);

            if (serviceResponse.Warnings.Count > 0 || serviceResponse.Warnings.Any())
                return BadResponse(new ResultModel
                {
                    Status = false,
                    Message = serviceResponse.Warnings.First()
                });

            return OkResponse(new PostCommentListDto
            {
                Text = serviceResponse.Data.Text,
                Id = serviceResponse.Data.Id,
                PostId = serviceResponse.Data.PostId,
                CreatedDate = serviceResponse.Data.CreatedDate,
                CreatedByUserName = serviceResponse.Data.CreatedByUserName,
                CreatedByUserPhoto = serviceResponse.Data.CreatedByUserPhoto
            });
        }

        #endregion
    }
}
