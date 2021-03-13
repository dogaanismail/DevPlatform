using System;
using CloudinaryDotNet;
using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Configuration;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Api;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using DevPlatform.Framework.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace DevPlatform.Api.Controllers
{
    [ApiController]
    public partial class PostsController : BaseApiController
    {
        #region Fields
        private readonly IPostService _postService;
        private CloudinaryConfig _cloudinaryOptions;
        private Cloudinary _cloudinary;
        private UserManager<AppUser> _userManager;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPostImageService _postImageService;
        private readonly IPostVideoService _postVideoService;
        private readonly IPostCommentService _postCommentService;
        #endregion

        #region Ctor
        public PostsController(IPostService postService,
            CloudinaryConfig cloudinaryOptions,
            UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IPostImageService postImageService,
            IPostVideoService postVideoService,
            IUserService userService,
            IPostCommentService postCommentService)
        {
            _postService = postService;
            _cloudinaryOptions = cloudinaryOptions;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _postImageService = postImageService;
            _postVideoService = postVideoService;
            _userService = userService;
            _postCommentService = postCommentService;

            Account account = new Account(
              _cloudinaryOptions.CloudName,
              _cloudinaryOptions.ApiKey,
              _cloudinaryOptions.ApiSecret);

            _cloudinary = new Cloudinary(account);
        }
        #endregion

        #region Methods

        [HttpPost("createpost")]
        [Authorize]
        public virtual JsonResult CreatePost([FromForm] PostCreateApi model)
        {
            var serviceResponse = _postService.Create(model);

            if (serviceResponse.Warnings.Count > 0)
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
            try
            {
                if (string.IsNullOrEmpty(model.Text))
                {
                    Result.Status = false;
                    Result.Message = "Comment text can not be null ! ";
                    return BadResponse(Result);
                }
                else
                {
                    var commentPost = _postService.GetById(model.PostId);
                    var appUser = _userService.FindByUserName(User.Identity.Name);
                    if (appUser == null)
                    {
                        return BadResponse(new ResultModel
                        {
                            Status = false,
                            Message = "User not found ! "
                        });
                    }

                    PostComment newComment = new PostComment
                    {
                        PostId = model.PostId,
                        Text = model.Text,
                        CreatedBy = appUser.Id
                    };

                    ResultModel result = _postCommentService.Create(newComment);
                    if (result.Status)
                    {
                        return OkResponse(new PostCommentListDto
                        {
                            Text = newComment.Text,
                            Id = newComment.Id,
                            PostId = newComment.PostId,
                            CreatedDate = newComment.CreatedDate,
                            CreatedByUserName = appUser.UserName,
                            CreatedByUserPhoto = appUser.UserDetail?.ProfilePhotoPath
                        });
                    }
                    else
                    {
                        Result.Status = false;
                        Result.Message = "Comment could not be added ! ";
                        return BadResponse(Result);
                    }
                }
            }
            catch (Exception ex)
            {
                Result.Status = false;
                Result.Message = ex.ToString();
                return BadResponse(Result);
            }

        }

        #endregion
    }
}
