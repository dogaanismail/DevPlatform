using DevPlatform.Business.Interfaces;
using DevPlatform.Domain.Api;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using DevPlatform.Domain.Dto.StoryDto;
using DevPlatform.Domain.Enumerations;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevPlatform.Api.Controllers
{
    [ApiController]
    public partial class PostsController : BaseApiController
    {
        #region Fields
        private readonly IPostService _postService;
        private readonly IPostCommentService _postCommentService;
        private readonly ILogService _logService;
        #endregion

        #region Ctor
        public PostsController(IPostService postService,
            IPostCommentService postCommentService,
            ILogService logService)
        {
            _postService = postService;
            _postCommentService = postCommentService;
            _logService = logService;
        }
        #endregion

        #region Methods

        [HttpPost("createpost")]
        [Authorize]
        public virtual JsonResult CreatePost([FromForm] PostCreateApi model)
        {
            _logService.InsertLogAsync(LogLevel.Information, $"PostsController- Create Post Request", JsonConvert.SerializeObject(model));

            var serviceResponse = _postService.Create(model);

            if (serviceResponse.Warnings.Count > 0 || serviceResponse.Warnings.Any())
            {
                _logService.InsertLogAsync(LogLevel.Error, $"PostsController- Create Post Error", JsonConvert.SerializeObject(serviceResponse));

                return BadResponse(new ResultModel
                {
                    Status = false,
                    Message = string.Join(Environment.NewLine, serviceResponse.Warnings.Select(err => string.Join(Environment.NewLine, err)))
                });
            }

            return OkResponse(new PostListDto
            {
                Id = serviceResponse.Data.Id,
                Text = serviceResponse.Data?.Text,
                ImageUrlList = serviceResponse.Data?.ImageUrlList,
                CreatedByUserName = serviceResponse.Data?.CreatedByUserName,
                CreatedByUserPhoto = serviceResponse.Data?.CreatedByUserPhoto,
                CreatedDate = serviceResponse.Data.CreatedDate,
                VideoUrl = serviceResponse.Data?.VideoUrl,
                PostType = serviceResponse.Data?.PostType,
                Comments = new List<PostCommentListDto>(),
                FancyboxData = $"post{serviceResponse.Data.Id}",
                StoryListDto = new()
                {
                    Id = serviceResponse.Data?.StoryCreateResponse?.Id,
                    Title = serviceResponse.Data.StoryCreateResponse?.Title,
                    Description = serviceResponse.Data?.StoryCreateResponse?.Description,
                    ImageUrl = serviceResponse.Data?.StoryCreateResponse?.ImageUrl,
                    VideoUrl = serviceResponse.Data?.StoryCreateResponse?.VideoUrl,
                    CreatedByUserName = serviceResponse.Data?.StoryCreateResponse?.CreatedByUserName,
                    CreatedByUserPhoto = serviceResponse.Data?.StoryCreateResponse?.CreatedByUserPhoto,
                    CreatedDate = serviceResponse.Data?.StoryCreateResponse?.CreatedDate,
                    StoryType = serviceResponse.Data?.StoryCreateResponse?.StoryType,
                    Comments = new List<StoryCommentListDto>()
                }
            });
        }

        [HttpGet("postlist")]
        [AllowAnonymous]
        public virtual JsonResult GetPostList()
        {
            var data = _postService.GetPostList();
            return OkResponse(data);
        }

        [HttpGet("id/{id}")]
        [AllowAnonymous]
        public virtual JsonResult GetById(int id)
        {
            var data = _postService.GetByIdAsDto(id);

            return OkResponse(data);
        }

        [HttpPost("createcomment")]
        [Authorize]
        public virtual JsonResult CreatePostComment([FromBody] PostCommentCreateApi model)
        {
            var serviceResponse = _postCommentService.Create(model);

            if (serviceResponse.Warnings.Count > 0 || serviceResponse.Warnings.Any())
            {
                _logService.InsertLogAsync(LogLevel.Error, $"PostsController- Create Post Comment Error", JsonConvert.SerializeObject(serviceResponse));

                return BadResponse(new ResultModel
                {
                    Status = false,
                    Message = string.Join(Environment.NewLine, serviceResponse.Warnings.Select(err => string.Join(Environment.NewLine, err))),
                });
            }

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
