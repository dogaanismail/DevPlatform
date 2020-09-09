using System;
using CloudinaryDotNet;
using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Configuration;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Api;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using DevPlatform.Domain.Enumerations;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace DevPlatform.Api.Controllers
{
    [ApiController]
    public class PostsController : BaseApiController
    {
        #region Fields
        private readonly IPostService _postService;
        private CloudinaryConfig _cloudinaryOptions;
        private Cloudinary _cloudinary;
        private UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Ctor
        public PostsController(IPostService postService, CloudinaryConfig cloudinaryOptions,
            UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _postService = postService;
            _cloudinaryOptions = cloudinaryOptions;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
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
                ResultModel postModel = _postService.Create(newPost);

                var user = _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name).Result;
                //var posts = _postService.GetUserPostsByUserId(user.Id);
                var appuser = _userManager.FindByIdAsync(user.Id.ToString()).Result;

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
