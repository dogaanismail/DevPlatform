﻿using DevPlatform.Business.Interfaces;
using DevPlatform.Core.Domain.Identity;
using DevPlatform.Core.Domain.Portal;
using DevPlatform.Domain.Common;
using DevPlatform.Domain.Dto;
using DevPlatform.Repository.Generic;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using DevPlatform.Domain.Api;
using DevPlatform.Common.Helpers;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using DevPlatform.Domain.Enumerations;
using System.Net;
using Microsoft.AspNetCore.Http;
using DevPlatform.Core.Configuration;
using DevPlatform.Domain.ServiceResponseModels.PostService;

namespace DevPlatform.Business.Services
{
    /// <summary>
    /// Post service
    /// </summary>
    public partial class PostService : ServiceExecute, IPostService
    {
        #region Fields
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<AppUser> _userRepository;
        private readonly IRepository<AppUserDetail> _userDetailRepository;
        private readonly IUserService _userService;
        private readonly IPostImageService _postImageService;
        private readonly IPostVideoService _postVideoService;
        private CloudinaryConfig _cloudinaryOptions;
        private Cloudinary _cloudinary;
        private readonly IImageProcessingService _imageProcessingService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Ctor
        public PostService(IRepository<Post> postRepository,
            IRepository<AppUser> userRepository,
            IRepository<AppUserDetail> userDetailRepository,
            IUserService userService,
            IPostImageService postImageService,
            IPostVideoService postVideoService,
            IHttpContextAccessor httpContextAccessor,
            CloudinaryConfig cloudinaryOptions,
            IImageProcessingService imageProcessingService)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _userDetailRepository = userDetailRepository;
            _userService = userService;
            _postImageService = postImageService;
            _postVideoService = postVideoService;
            _imageProcessingService = imageProcessingService;
            _cloudinaryOptions = cloudinaryOptions;
            _httpContextAccessor = httpContextAccessor;

            Account account = new(
            _cloudinaryOptions.CloudName,
            _cloudinaryOptions.ApiKey,
            _cloudinaryOptions.ApiSecret);

            _cloudinary = new Cloudinary(account);
        }
        #endregion

        #region Methods

        /// <summary>
        /// Deletes a post
        /// </summary>
        /// <param name="post"></param>
        public virtual void Delete(Post post)
        {
            if (post == null)
                throw new ArgumentNullException(nameof(post));

            _postRepository.Delete(post);
        }

        /// <summary>
        /// Gets a post by id
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public virtual Post GetById(int postId)
        {
            if (postId == 0)
                return null;

            return _postRepository.GetById(postId);
        }

        /// <summary>
        /// Inserts a post
        /// </summary>
        /// <param name="post"></param>
        public virtual ResultModel Create(Post post)
        {
            if (post == null)
                throw new ArgumentNullException(nameof(post));

            _postRepository.Insert(post);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        /// <summary>
        /// Inserts posts by using bulk
        /// </summary>
        /// <param name="posts"></param>
        /// <returns></returns>
        public ResultModel Create(List<Post> posts)
        {
            if (posts == null)
                throw new ArgumentNullException(nameof(posts));

            _postRepository.Insert(posts);
            return new ResultModel { Status = true, Message = "Create Process Success ! " };
        }

        /// <summary>
        /// Updates a post
        /// </summary>
        /// <param name="post"></param>
        public virtual void Update(Post post)
        {
            if (post == null)
                throw new ArgumentNullException(nameof(post));

            _postRepository.Update(post);
        }

        /// <summary>
        /// Returns a post by id as dto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PostListDto GetByIdAsDto(int id)
        {
            Post getPost = _postRepository.Table.
                LoadWith(x => x.PostImages)
                .LoadWith(x => x.PostVideos)
                .LoadWith(x => x.PostComments).ThenLoad(x => x.CreatedUser).ThenLoad(x => x.UserDetail)
                .LoadWith(x => x.CreatedUser).ThenLoad(x => x.UserDetail).FirstOrDefault(y => y.Id == id);

            PostListDto postListDto = new PostListDto
            {
                Id = getPost.Id,
                Text = getPost.Text,
                CreatedDate = getPost.CreatedDate,
                ImageUrl = getPost.PostImages.Count() == 0 ? "" : getPost.PostImages.FirstOrDefault().ImageUrl, //TODO It can be more photos of the post
                VideoUrl = getPost.PostVideos.Count() == 0 ? "" : getPost.PostVideos.FirstOrDefault().VideoUrl, //TODO It can be more videos of the post
                CreatedByUserName = getPost.CreatedUser == null ? "" : getPost.CreatedUser.UserName,
                CreatedByUserPhoto = getPost.CreatedUser.UserDetail.ProfilePhotoPath,
                PostType = getPost.PostType,
                Comments = getPost.PostComments.ToList().Select(y => new PostCommentListDto
                {
                    Text = y.Text,
                    CreatedDate = y.CreatedDate,
                    Id = y.Id,
                    CreatedByUserName = y.CreatedUser.UserName,
                    CreatedByUserPhoto = y.CreatedUser.UserDetail.ProfilePhotoPath,
                    PostId = y.PostId
                }).ToList()
            };

            return postListDto;
            //TODO must be reverted to LoadWith LinqToDB extension method
        }

        /// <summary>
        /// Returns a list of posts as dto
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PostListDto> GetPostList()
        {
            IEnumerable<PostListDto> data = _postRepository.Table
                .LoadWith(x => x.CreatedUser).ThenLoad(x => x.UserDetail)
                .LoadWith(x => x.PostImages).LoadWith(x => x.PostVideos)
                .LoadWith(x => x.PostComments).LoadWith(x => x.CreatedUser).ThenLoad(x => x.UserDetail)
                .Select(p => new PostListDto
                {
                    Id = p.Id,
                    Text = p.Text,
                    CreatedDate = p.CreatedDate,
                    ImageUrl = p.PostImages.Count() == 0 ? "" : p.PostImages.FirstOrDefault().ImageUrl, //TODO It can be more photos of the post
                    VideoUrl = p.PostVideos.Count() == 0 ? "" : p.PostVideos.FirstOrDefault().VideoUrl, //TODO It can be more videos of the post
                    CreatedByUserName = p.CreatedUser == null ? "" : p.CreatedUser.UserName,
                    CreatedByUserPhoto = p.CreatedUser == null ? "" : p.CreatedUser.UserDetail.ProfilePhotoPath,
                    PostType = p.PostType,
                    Comments = p.PostComments.ToList().Select(y => new PostCommentListDto
                    {
                        Text = y.Text,
                        CreatedDate = y.CreatedDate,
                        Id = y.Id,
                        CreatedByUserName = y.CreatedUser.UserName,
                        CreatedByUserPhoto = y.CreatedUser.UserDetail.ProfilePhotoPath,
                        PostId = y.PostId
                    }).ToList()

                    //TODO must be reverted to LoadWith LinqToDB extension method
                }).OrderByDescending(sa => sa.CreatedDate).AsEnumerable();

            return data;
        }

        /// <summary>
        /// Returns a list of post by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<Post> GetUserPostsByUserId(int userId)
        {
            IEnumerable<Post> getPost = _postRepository.Table.LoadWith(x => x.PostImages)
              .LoadWith(x => x.PostVideos).LoadWith(x => x.PostComments).ThenLoad(x => x.CreatedUser)
              .ThenLoad(x => x.UserDetail).LoadWith(x => x.CreatedUser).ThenLoad(x => x.UserDetail)
              .Where(y => y.CreatedBy == userId).ToList();

            return getPost;
        }

        /// <summary>
        /// Returns a list of post as dto by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<PostListDto> GetUserPostsWithDto(int userId)
        {
            IEnumerable<PostListDto> getPost = _postRepository.Table.LoadWith(x => x.PostImages)
             .LoadWith(x => x.PostVideos).LoadWith(x => x.PostComments).ThenLoad(x => x.CreatedUser)
             .ThenLoad(x => x.UserDetail).LoadWith(x => x.CreatedUser).ThenLoad(x => x.UserDetail)
             .Where(y => y.CreatedBy == userId)
              .Select(p => new PostListDto
              {
                  Id = p.Id,
                  Text = p.Text,
                  CreatedDate = p.CreatedDate,
                  ImageUrl = p.PostImages.Count() == 0 ? "" : p.PostImages.FirstOrDefault().ImageUrl, //TODO It can be more photos of the post
                  VideoUrl = p.PostVideos.Count() == 0 ? "" : p.PostVideos.FirstOrDefault().VideoUrl, //TODO It can be more videos of the post
                  CreatedByUserName = p.CreatedUser == null ? "" : p.CreatedUser.UserName,
                  CreatedByUserPhoto = p.CreatedUser == null ? "" : p.CreatedUser.UserDetail.ProfilePhotoPath,
                  PostType = p.PostType,
                  Comments = p.PostComments.ToList().Select(y => new PostCommentListDto
                  {
                      Text = y.Text,
                      CreatedDate = y.CreatedDate,
                      Id = y.Id,
                      CreatedByUserName = y.CreatedUser.UserName,
                      CreatedByUserPhoto = y.CreatedUser.UserDetail.ProfilePhotoPath,
                      PostId = y.PostId
                  }).ToList()

                  //TODO must be reverted to LoadWith LinqToDB extension method
              }).OrderByDescending(sa => sa.CreatedDate).AsEnumerable();

            return getPost;
        }

        /// <summary>
        /// Inserts posts and returns service response
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ServiceResponse<CreateResponse> Create(PostCreateApi model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var serviceResponse = new ServiceResponse<CreateResponse>
            {
                Success = false
            };

            try
            {
                if (string.IsNullOrEmpty(model.Text))
                    return ServiceResponse((CreateResponse)null, new List<string> { "Text can not be null !" });

                var appUser = _userService.FindByUserName(_httpContextAccessor.HttpContext.User.Identity.Name);
                if (appUser == null)
                    return ServiceResponse((CreateResponse)null, new List<string> { "User not found!" });

                bool hasImage = CheckItemType.HasItemImage(model);
                bool hasVideo = CheckItemType.HasItemVideo(model);
                var imageUploadResult = new ImageUploadResult();
                var videoUploadResult = new VideoUploadResult();

                #region CloudinaryProcess

                #region ImageUploadingProcess
                if (hasImage)
                {
                    imageUploadResult = _imageProcessingService.UploadImage(model.Photo);

                    if (imageUploadResult.Error != null)
                        return ServiceResponse((CreateResponse)null, new List<string> { imageUploadResult.Error.Message.ToString() });
                }

                #endregion

                #region VideoUploadingProcess

                if (hasVideo)
                {
                    videoUploadResult = _imageProcessingService.UploadVideo(model.Video);

                    if (videoUploadResult.Error != null)
                        return ServiceResponse((CreateResponse)null, new List<string> { videoUploadResult.Error.Message.ToString() });
                }

                #endregion

                #endregion

                #region POST CRUD

                var newPost = new Post
                {
                    Text = model.Text,
                    PostType = GetPostType(hasImage, hasVideo),
                    CreatedBy = appUser.Id
                };

                ResultModel postModel = Create(newPost);

                if (!postModel.Status)
                    return ServiceResponse((CreateResponse)null, new List<string> { postModel.Message });

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
                        return ServiceResponse((CreateResponse)null, new List<string> { postImageModel.Message });
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
                        return ServiceResponse((CreateResponse)null, new List<string> { postVideoModel.Message });
                }

                #endregion

                #endregion

                #region Story CRUD

                if (model.IsStory.GetValueOrDefault())
                {

                }

                #endregion

                serviceResponse.Success = true;
                serviceResponse.Data = new CreateResponse
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

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns PostType by PostTypeId
        /// </summary>
        /// <param name="hasImage"></param>
        /// <param name="hasVideo"></param>
        /// <returns></returns>
        private int GetPostType(bool hasImage, bool hasVideo)
        {
            return hasImage == true ? (int)PostTypeEnum.PostImage
                : hasVideo == true ? (int)PostTypeEnum.PostVideo
                : (int)PostTypeEnum.PostText;
        }

        #endregion

    }
}
