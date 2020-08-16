using System;
using CloudinaryDotNet;
using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Configuration;
using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Api;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using DevPlatform.Domain.Enumerations;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevPlatform.Api.Controllers
{
    [ApiController]
    public class PostsController : BaseApiController
    {
        #region Fields
        private readonly IPostService _postService;
        private CloudinaryConfig _cloudinaryOptions;
        private Cloudinary _cloudinary;
        #endregion

        #region Ctor
        public PostsController(IPostService postService, CloudinaryConfig cloudinaryOptions)
        {
            _postService = postService;
            _cloudinaryOptions = cloudinaryOptions;

            Account account = new Account(
              _cloudinaryOptions.CloudName,
              _cloudinaryOptions.ApiKey,
              _cloudinaryOptions.ApiSecret);

            _cloudinary = new Cloudinary(account);
        }
        #endregion

        [HttpPost("createpost")]
        [AllowAnonymous]
        public JsonResult CreatePost([FromForm] PostCreateApi model)
        {
            try
            {
                var newPost = new Post
                {
                    Text = model.Text,
                    PostType = (int)PostTypeEnum.PostImage,
                    CreatedBy = 1
                };
                ResultModel postModel = _postService.InsertPost(newPost);

                return OkResponse(new PostListDto
                {
                    Id = newPost.Id,
                    Text = newPost.Text,
                    ImageUrl = "gdgdg",
                    CreatedByUserName = "fdf",
                    CreatedByUserPhoto = "sf",
                    CreatedDate = newPost.CreatedDate,
                    VideoUrl = "fsf",
                    PostType = newPost.PostType,
                    Comments = null
                });
            }
            catch (Exception ex)
            {
                Result.Status = false;
                Result.Message = ex.ToString();
                return BadResponse(Result);
            }
        }
    }
}
