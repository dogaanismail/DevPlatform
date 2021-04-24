﻿using DevPlatform.Business.Interfaces;
using DevPlatform.Domain.Api;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using DevPlatform.Domain.Enumerations;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

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
            var serviceResponse = _postService.Create(model);

            if (serviceResponse.Warnings.Count > 0 || serviceResponse.Warnings.Any())
                return BadResponse(new ResultModel
                {
                    Status = false,
                    Message = string.Join(Environment.NewLine, serviceResponse.Warnings.Select(err => string.Join(Environment.NewLine, err))),
                });

            _logService.InsertLogAsync(LogLevel.Information, $"PostsController- Create Post Request", JsonConvert.SerializeObject(model));

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
                Comments = null,
                StoryListDto = new()
                {
                    Id = serviceResponse.Data?.StoryCreateResponse?.Id,
                    Title = serviceResponse.Data.StoryCreateResponse?.Title,
                    Description = serviceResponse.Data?.StoryCreateResponse?.Description,
                    ImageUrl = serviceResponse.Data?.StoryCreateResponse?.ImageUrl,
                    VideoUrl = serviceResponse.Data?.StoryCreateResponse?.VideoUrl,
                    CreatedByUserName = serviceResponse.Data?.StoryCreateResponse.CreatedByUserName,
                    CreatedByUserPhoto = serviceResponse.Data?.StoryCreateResponse?.CreatedByUserPhoto,
                    CreatedDate = serviceResponse.Data?.StoryCreateResponse?.CreatedDate,
                    StoryType = serviceResponse.Data?.StoryCreateResponse?.StoryType,
                    Comments = null
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

        [HttpPost("createcomment")]
        [Authorize]
        public virtual JsonResult CreatePostComment([FromBody] PostCommentCreateApi model)
        {
            var serviceResponse = _postCommentService.Create(model);

            if (serviceResponse.Warnings.Count > 0 || serviceResponse.Warnings.Any())
                return BadResponse(new ResultModel
                {
                    Status = false,
                    Message = string.Join(Environment.NewLine, serviceResponse.Warnings.Select(err => string.Join(Environment.NewLine, err))),
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
