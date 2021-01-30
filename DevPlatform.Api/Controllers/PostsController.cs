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
using System.Net;
using CloudinaryDotNet.Actions;
using DevPlatform.Common.Helpers;

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
            try
            {
                if (string.IsNullOrEmpty(model.Text))
                {
                    Result.Status = false;
                    Result.Message = "You can not add a post without writing text ! ";
                    return BadResponse(Result);
                }
                var appUser = _userService.FindByUserName(User.Identity.Name);
                if (appUser == null)

                {
                    return BadResponse(new ResultModel
                    {
                        Status = false,
                        Message = "User not found !"
                    });
                }

                bool hasImage = CheckItemType.HasItemImage(model);
                bool hasVideo = CheckItemType.HasItemVideo(model);
                var imageUploadResult = new ImageUploadResult();
                var videoUploadResult = new VideoUploadResult();

                #region CloudinaryProcess

                #region ImageUploadingProcess
                if (hasImage)
                {
                    using (var stream = model.Photo.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams
                        {
                            File = new FileDescription(model.Photo.Name, stream)
                        };

                        imageUploadResult = _cloudinary.Upload(uploadParams);
                        if (imageUploadResult.Error != null)
                        {
                            return BadResponse(ResultModel.Error("The upload process can not be done !"));
                        }
                    }
                }
                #endregion

                #region VideoUploadingProcess

                if (hasVideo)
                {
                    using (var stream = model.Video.OpenReadStream())
                    {
                        var uploadParams = new VideoUploadParams
                        {
                            File = new FileDescription(model.Video.Name, stream)
                        };

                        videoUploadResult = _cloudinary.Upload(uploadParams);
                        if (videoUploadResult.Error != null)
                        {
                            return BadResponse(ResultModel.Error("The upload process can not be done !"));
                        }
                    }
                }

                #endregion

                #endregion

                #region CRUD

                var newPost = new Post
                {
                    Text = model.Text,
                    PostType = hasImage == true ? (int)PostTypeEnum.PostImage
                    : hasVideo == true ? (int)PostTypeEnum.PostVideo
                    : (int)PostTypeEnum.PostText,
                    CreatedBy = appUser.Id
                };
                ResultModel postModel = _postService.Create(newPost);

                if (!postModel.Status)
                {
                    return BadResponse(ResultModel.Error("The upload process can not be done !"));
                }

                #region PostImages
                if (imageUploadResult != null && imageUploadResult.StatusCode == HttpStatusCode.OK)
                {
                    var postImages = new PostImage
                    {
                        PostId = newPost.Id,
                        ImageUrl = imageUploadResult.Url.ToString()
                    };
                    ResultModel postImageModel = _postImageService.Create(postImages);

                    if (!postImageModel.Status)
                    {
                        return BadResponse(ResultModel.Error("The upload process can not be done !"));
                    }
                }

                #endregion

                #region PostVideos

                if (videoUploadResult != null && videoUploadResult.StatusCode == HttpStatusCode.OK)
                {
                    var postVideos = new PostVideo
                    {
                        PostId = newPost.Id,
                        VideoUrl = videoUploadResult.Url.ToString()
                    };
                    ResultModel postVideoModel = _postVideoService.Create(postVideos);

                    if (!postVideoModel.Status)
                    {
                        return BadResponse(ResultModel.Error("The upload process can not be done !"));
                    }
                }

                #endregion

                #endregion

                //TODO
                //There must be an integration that returns the last post that has just been createad.

                return OkResponse(new PostListDto
                {
                    Id = newPost.Id,
                    Text = newPost.Text,
                    ImageUrl = imageUploadResult.Url?.ToString(),
                    CreatedByUserName = appUser.UserName,
                    CreatedByUserPhoto = appUser.UserDetail.ProfilePhotoPath,
                    CreatedDate = newPost.CreatedDate,
                    VideoUrl = videoUploadResult.Url?.ToString(),
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

        [HttpGet("postlist")]
        [AllowAnonymous]
        public JsonResult GetPostList()
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
                            CreatedByUserPhoto = appUser.UserDetail.ProfilePhotoPath
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
